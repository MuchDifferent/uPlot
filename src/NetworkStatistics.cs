#region USINGS

using UnityEngine;

#endregion USINGS

/// <summary>
/// This class is ued for storing network statistics over time since we can not use uLink's class due to the amount of access that we have to it.
/// </summary>
public class NetworkStatistics
{
	#region PUBLIC_VARIABLES

	public float time;
	public int connectionsCount;
	public double bytesReceived;
	public double bytesReceivedPerSecond;
	public double bytesSent;
	public double bytesSentPerSecond;
	public long messageDuplicatesRejected;
	public long messageSequencesRejected;
	public long messagesReceived;
	public long messagesResent;
	public long messagesSent;
	public long messagesStored;
	public long messagesUnsent;
	public long messagesWithheld;
	public long packetsReceived;
	public long packetsSent;
	public double userBytesReceived;
	public double userBytesReceivedPerSecond;
	public double userBytesSent;
	public double userBytesSentPerSecond;

	#endregion PUBLIC_VARIABLES

	#region PRIVATE_VARIABLES

	private static int oldFrameCount;
	private double lastBytesSent;
	private double lastBytesReceived;
	private double lastUserBytesSent;
	private double lastUserBytesReceived;
	private float lastTime;

	#endregion PRIVATE_VARIABLES

	#region PUBLIC_CONSTRUCTOR

	public NetworkStatistics()
	{
	}

	#endregion PUBLIC_CONSTRUCTOR

	#region PUBLIC_METHODS

	public void CalculateNetworkStats()
	{
		this.time = Time.time;
		this.connectionsCount = uLink.Network.connections.Length;

		foreach (var player in uLink.Network.connections)
		{
			var stats = uLink.Network.GetStatistics(player);
			if (stats != null)
			{
				this.bytesReceived += stats.bytesReceived;
				this.bytesSent += stats.bytesSent;
				this.messageDuplicatesRejected += stats.messageDuplicatesRejected;
				this.messageSequencesRejected += stats.messageSequencesRejected;
				this.messagesReceived += stats.messagesReceived;
				this.messagesResent += stats.messagesResent;
				this.messagesSent += stats.messagesSent;
				this.messagesStored += stats.messagesStored;
				this.messagesUnsent += stats.messagesUnsent;
				this.messagesWithheld += stats.messagesWithheld;
				this.packetsReceived += stats.packetsReceived;
				this.packetsSent += stats.packetsSent;
				this.userBytesReceived += stats.userBytesReceived;
				this.userBytesReceivedPerSecond += stats.userBytesReceivedPerSecond;
				this.userBytesSent += stats.userBytesSent;
				this.userBytesSentPerSecond += stats.userBytesSentPerSecond;
			}
		}

		var deltaTime = Time.time - lastTime;

		this.bytesReceivedPerSecond =
			(this.bytesReceived - lastBytesReceived) / deltaTime;
		lastBytesReceived = this.bytesReceived;
		this.bytesSentPerSecond =
			(this.bytesSent - lastBytesSent) / deltaTime;
		lastBytesSent = this.bytesSent;
		this.userBytesReceivedPerSecond =
			(this.userBytesReceived - lastUserBytesReceived) / deltaTime;
		lastUserBytesReceived = this.userBytesReceived;
		this.userBytesSentPerSecond =
			(this.userBytesSent - lastUserBytesSent) / deltaTime;
		lastUserBytesSent = this.userBytesSent;

		lastTime = Time.time;
	}

	#endregion PUBLIC_METHODS
}