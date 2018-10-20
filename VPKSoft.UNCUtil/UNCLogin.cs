#region License
/*
VPKSoft.UNCUtil

An utility to access Windows (SMB/CIFS) shares with credentials.
Copyright © 2018 VPKSoft, Petteri Kautonen

Contact: vpksoft@vpksoft.net

This file is part of VPKSoft.UNCUtil.

VPKSoft.UNCUtil is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

VPKSoft.UNCUtil is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with VPKSoft.UNCUtil.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;

#pragma warning disable CS1587 // XML comment is not placed on a valid language element
/// <summary>
/// The name space for the UNCLogin class and its members.
/// </summary>
namespace VPKSoft.UNCUtil // the Sandcastle did require the name space to be commented as well once..
#pragma warning restore CS1587 // XML comment is not placed on a valid language element
{
    /// <summary>
    /// A class to help copy / move files from Windows file shares (SMB / CIFS) with credentials.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class UNCLogin : IDisposable // Source: https://docs.microsoft.com/en-us/dotnet/api/system.idisposable?view=netframework-4.6.1
    {
        /// <summary>
        /// Gets or sets the name of the network. I.e. LOCALHOST or 192.168.1.105, etc.
        /// </summary>
        private string NetworkName { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="UNCLogin"/> class.
        /// </summary>
        /// <param name="networkName">Name of the network. I.e. LOCALHOST or 192.168.1.105, etc.</param>
        /// <param name="credential">A NetworkCredential class instance to be used to access a remote (SMB / CIFS) share.</param>
        /// <exception cref="Win32Exception">This exception is thrown if the WNetAddConnection2 function returns an error.</exception>
        public UNCLogin(string networkName, NetworkCredential credential)
        {
            NetworkName = networkName; // save the network name for further use..
            NetResource netResource = new NetResource() // create a new NetResource class instance..
            {
                RemoteName = networkName // ..and set the network name..
            };

            // save the result of the WNetAddConnection2 call
            int result = WNetAddConnection2(netResource.NetResourceValue, credential.Password, credential.UserName, 0);

            // a WinApi call always returns non-zero, so let the non-zero value to be interpreted as
            // a Win32Exception..
            if (result != 0)
            {
                // save the last Win32Exception if any..
                try // ..and do try so the library won't fail on this one..
                {
                    LastWin32Exception = new Win32Exception(result);
                }
                catch // an error, so an empty exception..
                {
                    LastWin32Exception = new Win32Exception();
                }

                // this is fun (always)..
                throw new Win32Exception(result);
            }
        }

        /// <summary>
        /// The WNetAddConnection2 function makes a connection to a network resource and can redirect a local device to the network resource.
        /// </summary>
        /// <param name="netResource">A NETRESOURCE structure that specifies details of the proposed connection, such as information about the network resource, the local device, and the network resource provider.</param>
        /// <param name="password">A null-terminated string that specifies a password to be used in making the network connection.</param>
        /// <param name="userName">A null-terminated string that specifies a user name for making the connection.</param>
        /// <param name="flags">A set of connection options. (See: https://docs.microsoft.com/en-us/windows/desktop/api/winnetwk/nf-winnetwk-wnetaddconnection2a). </param>
        /// <returns>Zero if the operation was successful; otherwise a system error code which is translatable as Win32Exception.</returns>
        [DllImport("mpr.dll")] // P/Invoke :-( 
        internal static extern int 
            WNetAddConnection2(
                NetResource.NETRESOURCE netResource, 
                string password, string userName, int flags);

        /// <summary>
        /// The WNetCancelConnection2 function cancels an existing network connection. You can also call the function to remove remembered network connections that are not currently connected.
        /// </summary>
        /// <param name="name">A null-terminated string that specifies the name of either the redirected local device or the remote network resource to disconnect from.</param>
        /// <param name="flags">Connection type. (See: https://docs.microsoft.com/en-us/windows/desktop/api/winnetwk/nf-winnetwk-wnetcancelconnection2a). </param>
        /// <param name="force">Specifies whether the disconnection should occur if there are open files or jobs on the connection. If this parameter is false, the function fails if there are open files or jobs.</param>
        /// <returns>Zero if the operation was successful; otherwise a system error code which is translatable as Win32Exception.</returns>
        [DllImport("mpr.dll")] // P/Invoke :-( 
        internal static extern int WNetCancelConnection2(string name, int flags, bool force);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing) // Source: https://docs.microsoft.com/en-us/dotnet/api/system.idisposable?view=netframework-4.6.1
        {
            // cancel the network connection (i.e. free the used resources)..
            int result = WNetCancelConnection2(NetworkName, 0, true);

            if (result != 0) // save the last Win32Exception if any..
            {
                try // ..and do try so the library won't fail on this one..
                {
                    LastWin32Exception = new Win32Exception(result);
                }
                catch // an error, so an empty exception..
                {
                    LastWin32Exception = new Win32Exception();
                }
            }
        }

        /// <summary>
        /// Gets or sets the last Win32Exception exception which occurred either with the WNetAddConnection2 or WNetCancelConnection2 function calls.
        /// </summary>
        public static Win32Exception LastWin32Exception { get; set; } = null;

        /// <summary>
        /// Finalizes an instance of the <see cref="UNCLogin"/> class.
        /// </summary>
        ~UNCLogin()
        {
            // something about: Do not re-create Dispose clean-up code here..
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() // Source: https://docs.microsoft.com/en-us/dotnet/api/system.idisposable?view=netframework-4.6.1
        {
            Dispose(true);
            // Source: https://docs.microsoft.com/en-us/dotnet/api/system.gc.suppressfinalize?view=netframework-4.6.1
            GC.SuppressFinalize(this);
        }
    }
}
