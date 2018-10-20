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

using System.Runtime.InteropServices;

#pragma warning disable CS1587 // XML comment is not placed on a valid language element
/// <summary>
/// The name space for the UNCLogin class and its members.
/// </summary>
namespace VPKSoft.UNCUtil // the Sandcastle did require the name space to be commented as well once..
#pragma warning restore CS1587 // XML comment is not placed on a valid language element
{
    // Source: https://msdn.microsoft.com/en-us/c53d078e-188a-4371-bdb9-fc023bc0c1ba

    /// <summary>
    /// The NETRESOURCE structure contains information about a network resource.
    /// </summary>
    public class NetResource
    {
        /// <summary>
        /// The scope of the enumeration.
        /// </summary>
        public ResourceScope ResourceScope { get; set; } = ResourceScope.ResourceScopeGlobalNet; // default to access remote share i.e. \\server\share..

        /// <summary>
        /// The type of resource.
        /// </summary>
        public ResourceType ResourceType { get; set; } = ResourceType.ResourceTypeDisk; // default to access remote share i.e. \\server\share..

        /// <summary>
        /// The display options for the network object in a network browsing user interface.
        /// </summary>
        public ResourceDisplayType ResourceDisplayType { get; set; } = ResourceDisplayType.ResourceDisplayTypeShare; // default to access remote share i.e. \\server\share..

        /// <summary>
        /// A set of bit flags describing how the resource can be used.
        /// </summary>
        public ResourceUsage ResourceUsage { get; set; } = ResourceUsage.ResourceUsageNone; // default to access remote share i.e. \\server\share..

        /// <summary>
        /// A null-terminated character string that specifies the name of a local device.
        /// </summary>
        public string LocalName { get; set; } // string's default value is null..

        /// <summary>
        /// A null-terminated character string that specifies the remote network name.
        /// </summary>
        public string RemoteName { get; set; } // string's default value is null..

        /// <summary>
        /// A null-terminated string that contains a comment supplied by the network provider.
        /// </summary>
        public string Comment { get; set; } // string's default value is null..

        /// <summary>
        /// A null-terminated string that contains the name of the provider that owns the resource.
        /// </summary>
        public string Provider { get; set; } // string's default value is null..

        /// <summary>
        /// Gets the actual sequentially laid out NETRESOURCE class which is used with the WNetAddConnection function.
        /// </summary>
        public NETRESOURCE NetResourceValue => new NETRESOURCE
        {
            // just assign the values as they are..
            Scope = ResourceScope,
            Type = ResourceType,
            DisplayType = ResourceDisplayType,
            Usage = ResourceUsage,
            LocalName = LocalName,
            RemoteName = RemoteName,
            Comment = Comment,
            Provider = Provider
            // END: just assign the values as they are..
        };

        /// <summary>
        /// A sequentially laid out NETRESOURCE class which is used with the WNetAddConnection function.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class NETRESOURCE
        {
            /// <summary>
            /// The scope of the enumeration.
            /// </summary>
            public ResourceScope Scope;

            /// <summary>
            /// The type of resource.
            /// </summary>
            public ResourceType Type;

            /// <summary>
            /// The display options for the network object in a network browsing user interface.
            /// </summary>
            public ResourceDisplayType DisplayType;

            /// <summary>
            /// A set of bit flags describing how the resource can be used.
            /// </summary>
            public ResourceUsage Usage;

            /// <summary>
            /// A null-terminated character string that specifies the name of a local device.
            /// </summary>
            public string LocalName;

            /// <summary>
            /// A null-terminated string that contains a comment supplied by the network provider.
            /// </summary>
            public string RemoteName;

            /// <summary>
            /// A null-terminated string that contains a comment supplied by the network provider.
            /// </summary>
            public string Comment;

            /// <summary>
            /// A null-terminated string that contains the name of the provider that owns the resource.
            /// </summary>
            public string Provider;
        }
    }
}
