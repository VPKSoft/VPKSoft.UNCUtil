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

#pragma warning disable CS1587 // XML comment is not placed on a valid language element
/// <summary>
/// The name space for the UNCLogin class and its members.
/// </summary>
namespace VPKSoft.UNCUtil // the Sandcastle did require the name space to be commented as well once..
#pragma warning restore CS1587 // XML comment is not placed on a valid language element
{
    // Source: https://docs.microsoft.com/en-us/windows/desktop/api/winnetwk/nf-winnetwk-wnetaddconnection2a

    /// <summary>
    /// The type of resource.
    /// </summary>
    [Flags]
    public enum ResourceType : int
    {
        /// <summary>
        /// All resources.
        /// </summary>
        ResourceTypeAny = 0,

        /// <summary>
        /// Disk resources.
        /// </summary>
        ResourceTypeDisk = 1,

        /// <summary>
        /// Print resources.
        /// </summary>
        ResourceTypePrint = 2,

        /// <summary>
        /// No documentation found.
        /// </summary>
        ResourceTypeReserved = 8,

        /// <summary>
        /// No documentation found.
        /// </summary>
        ResourceTypeUnknown = -1
    }

    /// <summary>
    /// The scope of the network resource enumeration.
    /// </summary>
    public enum ResourceScope : int
    {
        /// <summary>
        /// Enumerate currently connected resources.
        /// </summary>
        ResourceScopeConnected = 1,

        /// <summary>
        /// Enumerate all resources on the network.
        /// </summary>
        ResourceScopeGlobalNet = 2,

        /// <summary>
        /// Enumerate remembered (persistent) connections.
        /// </summary>
        ResourceScopeRemembered = 3,

        /// <summary>
        /// No documentation found.
        /// </summary>
        ResourceScopeRecent = 4,

        /// <summary>
        /// No documentation found.
        /// </summary>
        ResourceScopeContext = 5
    }

    /// <summary>
    /// The display options for the network object in a network browsing user interface. 
    /// </summary>
    public enum ResourceDisplayType: int
    {
        /// <summary>
        /// The method used to display the object does not matter.
        /// </summary>
        ResourceDisplayTypeGeneric = 0,

        /// <summary>
        /// The object should be displayed as a domain.
        /// </summary>
        ResourceDisplayTypeDomain = 1,

        /// <summary>
        /// The object should be displayed as a server.
        /// </summary>
        ResourceDisplayTypeServer = 2,

        /// <summary>
        /// The object should be displayed as a share.
        /// </summary>
        ResourceDisplayTypeShare = 3,

        /// <summary>
        /// The object should be displayed as a file.
        /// </summary>
        ResourceDisplayTypeFile = 4,

        /// <summary>
        /// The object should be displayed as a group.
        /// </summary>
        ResourceDisplayTypeGroup = 5,

        /// <summary>
        /// The object should be displayed as a network.
        /// </summary>
        ResourceDisplayTypeNetwork = 6,

        /// <summary>
        /// The object should be displayed as a logical root for the entire network.
        /// </summary>
        ResourceDisplayTypeRoot = 7,

        /// <summary>
        /// he object should be displayed as a administrative share.
        /// </summary>
        ResourceDisplayTypeShareAdmin = 8,

        /// <summary>
        /// The object should be displayed as a directory.
        /// </summary>
        ResourceDisplayTypeDirectory = 9,

        /// <summary>
        /// The object should be displayed as a tree. This display type was used for a NetWare Directory Service (NDS) tree by the NetWare Workstation service supported on Windows XP and earlier.
        /// </summary>
        ResourceDisplayTypeTree = 0xA,

        /// <summary>
        /// The object should be displayed as a Netware Directory Service container. This display type was used by the NetWare Workstation service supported on Windows XP and earlier.
        /// </summary>
        ResourceDisplayTypeNDSContainer = 0xA
    }

    // Source: https://msdn.microsoft.com/en-us/c53d078e-188a-4371-bdb9-fc023bc0c1ba

    /// <summary>
    /// A set of bit flags describing how the resource can be used.
    /// </summary>
    public enum ResourceUsage : int
    {
        /// <summary>
        /// No documentation found. Possibly means there is no need to give a value for this integer enumeration.
        /// </summary>
        ResourceUsageNone = 0,

        /// <summary>
        /// The resource is a connect-able resource.
        /// </summary>
        ResourceUsageConnectable = 1,

        /// <summary>
        /// The resource is a container resource.
        /// </summary>
        ResourceUsageContainer = 2,

        /// <summary>
        /// The resource is not a local device.
        /// </summary>
        ResourceUsageNoLocalDevice = 4,

        /// <summary>
        /// The resource is a sibling. This value is not used by Windows.
        /// </summary>
        ResourceUsageSibling = 8,

        /// <summary>
        /// The resource must be attached.
        /// </summary>
        ResourceUsageAttached = 0x1000,

        /// <summary>
        /// A combination of ResourceUsageConnectable, ResourceUsageContainer and ResourceUsageAttached enumerations.
        /// </summary>
        ResourceUsageAll = 
            ResourceUsage.ResourceUsageConnectable | 
            ResourceUsage.ResourceUsageContainer | 
            ResourceUsageAttached,

        /// <summary>
        /// A reserved value. No documentation found.
        /// </summary>
        ResourceUsageReserved = 0x8000000
    }

    /// <summary>
    /// A set of connection options.
    /// </summary>
    public enum ConnectFlags : int
    {
        /// <summary>
        /// The network resource connection should be remembered.
        /// </summary>
        ConnectUpdateProfile = 0x0001,

        /// <summary>
        /// The network resource connection should not be put in the recent connection list.
        /// </summary>
        ConnectUpdateRecent = 0x0002,

        /// <summary>
        /// The network resource connection should not be remembered.
        /// </summary>
        ConnectTemporary = 0x0004,

        /// <summary>
        /// If this flag is set, the operating system may interact with the user for authentication purposes.
        /// </summary>
        ConnectInteractive = 0x0008,

        /// <summary>
        /// This flag instructs the system not to use any default settings for user names or passwords without offering the user the opportunity to supply an alternative.
        /// </summary>
        ConnectPrompt = 0x0010,

        /// <summary>
        /// This flag forces the redirection of a local device when making the connection.
        /// </summary>
        ConnectRedirect = 0x0080,

        /// <summary>
        /// If this flag is set, then the operating system does not start to use a new media to try to establish the connection (initiate a new dial up connection, for example).
        /// </summary>
        ConnectCurrentMedia = 0x0200,

        /// <summary>
        /// If this flag is set, the operating system prompts the user for authentication using the command line instead of a graphical user interface (GUI).
        /// </summary>
        ConnectCommandline = 0x0800,

        /// <summary>
        /// If this flag is set, and the operating system prompts for a credential, the credential should be saved by the credential manager.
        /// </summary>
        ConnectCMDSaveCredentials = 0x1000,

        /// <summary>
        /// If this flag is set, and the operating system prompts for a credential, the credential is reset by the credential manager.
        /// </summary>
        ConnectResetCredentials = 0x2000
    }
}
