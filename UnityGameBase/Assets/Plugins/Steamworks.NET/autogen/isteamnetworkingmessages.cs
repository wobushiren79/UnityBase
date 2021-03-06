// This file is provided under The MIT License as part of Steamworks.NET.
// Copyright (c) 2013-2019 Riley Labrecque
// Please see the included LICENSE.txt for additional information.

// This file is automatically generated.
// Changes to this file will be reverted when you update Steamworks.NET

#if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
	#define DISABLESTEAMWORKS
#endif

#if !DISABLESTEAMWORKS

using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace Steamworks {
	public static class SteamNetworkingMessages {
		/// <summary>
		/// <para>/ Sends a message to the specified host.  If we don't already have a session with that user,</para>
		/// <para>/ a session is implicitly created.  There might be some handshaking that needs to happen</para>
		/// <para>/ before we can actually begin sending message data.  If this handshaking fails and we can't</para>
		/// <para>/ get through, an error will be posted via the callback SteamNetworkingMessagesSessionFailed_t.</para>
		/// <para>/ There is no notification when the operation succeeds.  (You should have the peer send a reply</para>
		/// <para>/ for this purpose.)</para>
		/// <para>/</para>
		/// <para>/ Sending a message to a host will also implicitly accept any incoming connection from that host.</para>
		/// <para>/</para>
		/// <para>/ nSendFlags is a bitmask of k_nSteamNetworkingSend_xxx options</para>
		/// <para>/</para>
		/// <para>/ nRemoteChannel is a routing number you can use to help route message to different systems.</para>
		/// <para>/ You'll have to call ReceiveMessagesOnChannel() with the same channel number in order to retrieve</para>
		/// <para>/ the data on the other end.</para>
		/// <para>/</para>
		/// <para>/ Using different channels to talk to the same user will still use the same underlying</para>
		/// <para>/ connection, saving on resources.  If you don't need this feature, use 0.</para>
		/// <para>/ Otherwise, small integers are the most efficient.</para>
		/// <para>/</para>
		/// <para>/ It is guaranteed that reliable messages to the same host on the same channel</para>
		/// <para>/ will be be received by the remote host (if they are received at all) exactly once,</para>
		/// <para>/ and in the same order that they were send.</para>
		/// <para>/</para>
		/// <para>/ NO other order guarantees exist!  In particular, unreliable messages may be dropped,</para>
		/// <para>/ received out of order with respect to each other and with respect to reliable data,</para>
		/// <para>/ or may be received multiple times.  Messages on different channels are *not* guaranteed</para>
		/// <para>/ to be received in the order they were sent.</para>
		/// <para>/</para>
		/// <para>/ A note for those familiar with TCP/IP ports, or converting an existing codebase that</para>
		/// <para>/ opened multiple sockets:  You might notice that there is only one channel, and with</para>
		/// <para>/ TCP/IP each endpoint has a port number.  You can think of the channel number as the</para>
		/// <para>/ *destination* port.  If you need each message to also include a "source port" (so the</para>
		/// <para>/ recipient can route the reply), then just put that in your message.  That is essentially</para>
		/// <para>/ how UDP works!</para>
		/// <para>/</para>
		/// <para>/ Returns:</para>
		/// <para>/ - k_EREsultOK on success.</para>
		/// <para>/ - k_EResultNoConnection will be returned if the session has failed or was closed by the peer,</para>
		/// <para>/   and k_nSteamNetworkingSend_AutoRestartBrokwnSession is not used.  (You can use</para>
		/// <para>/   GetSessionConnectionInfo to get the details.)  In order to acknowledge the broken session</para>
		/// <para>/   and start a new one, you must call CloseSessionWithUser</para>
		/// <para>/ - See SendMessageToConnection::SendMessageToConnection for more</para>
		/// </summary>
		public static EResult SendMessageToUser(ref SteamNetworkingIdentity identityRemote, IntPtr pubData, uint cubData, int nSendFlags, int nRemoteChannel) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_SendMessageToUser(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote, pubData, cubData, nSendFlags, nRemoteChannel);
		}

		/// <summary>
		/// <para>/ Reads the next message that has been sent from another user via SendMessageToUser() on the given channel.</para>
		/// <para>/ Returns number of messages returned into your list.  (0 if no message are available on that channel.)</para>
		/// <para>/</para>
		/// <para>/ When you're done with the message object(s), make sure and call Release()!</para>
		/// </summary>
		public static int ReceiveMessagesOnChannel(int nLocalChannel, IntPtr[] ppOutMessages, int nMaxMessages) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_ReceiveMessagesOnChannel(CSteamAPIContext.GetSteamNetworkingMessages(), nLocalChannel, ppOutMessages, nMaxMessages);
		}

		/// <summary>
		/// <para>/ AcceptSessionWithUser() should only be called in response to a SteamP2PSessionRequest_t callback</para>
		/// <para>/ SteamP2PSessionRequest_t will be posted if another user tries to send you a message, and you haven't</para>
		/// <para>/ tried to talk to them.  If you don't want to talk to them, just ignore the request.</para>
		/// <para>/ If the user continues to send you messages, SteamP2PSessionRequest_t callbacks will continue to</para>
		/// <para>/ be posted periodically.  This may be called multiple times for a single user.</para>
		/// <para>/</para>
		/// <para>/ Calling SendMessage() on the other user, this implicitly accepts any pending session request.</para>
		/// </summary>
		public static bool AcceptSessionWithUser(ref SteamNetworkingIdentity identityRemote) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_AcceptSessionWithUser(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote);
		}

		/// <summary>
		/// <para>/ Call this when you're done talking to a user to immediately free up resources under-the-hood.</para>
		/// <para>/ If the remote user tries to send data to you again, another P2PSessionRequest_t callback will</para>
		/// <para>/ be posted.</para>
		/// <para>/</para>
		/// <para>/ Note that sessions that go unused for a few minutes are automatically timed out.</para>
		/// </summary>
		public static bool CloseSessionWithUser(ref SteamNetworkingIdentity identityRemote) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_CloseSessionWithUser(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote);
		}

		/// <summary>
		/// <para>/ Call this  when you're done talking to a user on a specific channel.  Once all</para>
		/// <para>/ open channels to a user have been closed, the open session to the user will be</para>
		/// <para>/ closed, and any new data from this user will trigger a SteamP2PSessionRequest_t</para>
		/// <para>/ callback</para>
		/// </summary>
		public static bool CloseChannelWithUser(ref SteamNetworkingIdentity identityRemote, int nLocalChannel) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_CloseChannelWithUser(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote, nLocalChannel);
		}

		/// <summary>
		/// <para>/ Returns information about the latest state of a connection, if any, with the given peer.</para>
		/// <para>/ Primarily intended for debugging purposes, but can also be used to get more detailed</para>
		/// <para>/ failure information.  (See SendMessageToUser and k_nSteamNetworkingSend_AutoRestartBrokwnSession.)</para>
		/// <para>/</para>
		/// <para>/ Returns the value of SteamNetConnectionInfo_t::m_eState, or k_ESteamNetworkingConnectionState_None</para>
		/// <para>/ if no connection exists with specified peer.  You may pass nullptr for either parameter if</para>
		/// <para>/ you do not need the corresponding details.  Note that sessions time out after a while,</para>
		/// <para>/ so if a connection fails, or SendMessageToUser returns SendMessageToUser, you cannot wait</para>
		/// <para>/ indefinitely to obtain the reason for failure.</para>
		/// </summary>
		public static ESteamNetworkingConnectionState GetSessionConnectionInfo(ref SteamNetworkingIdentity identityRemote, out SteamNetConnectionInfo_t pConnectionInfo, out SteamNetworkingQuickConnectionStatus pQuickStatus) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingMessages_GetSessionConnectionInfo(CSteamAPIContext.GetSteamNetworkingMessages(), ref identityRemote, out pConnectionInfo, out pQuickStatus);
		}
	}
}

#endif // !DISABLESTEAMWORKS
