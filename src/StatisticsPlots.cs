#region USINGS

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion USINGS

public class StatisticsPlots : MonoBehaviour
{
	#region PUBLIC_VARIABLES

	/// <summary>
	/// Frequency of saving statistics.
	/// </summary>
	public float saveFrequency = 1;

	/// <summary>
	/// The path which you want to save plots.
	/// If you leave it empty, plots will be saved in default path (Root of project folder).
	/// </summary>
	public string savePath = "";

	/// <summary>
	/// Data line color.
	/// </summary>
	public Color lineColor = Color.blue;

	/// <summary>
	/// Background color.
	/// </summary>
	public Color backColor = Color.gray;

	/// <summary>
	/// Plot background color.
	/// </summary>
	public Color plotBackColor = Color.white;

	/// <summary>
	/// Legend background color.
	/// </summary>
	public Color legendBackColor = Color.yellow;

	#endregion PUBLIC_VARIABLES

	#region PRIVATE_VARIABLES

	private List<NetworkStatistics> m_statisticsList;

	#endregion PRIVATE_VARIABLES

	#region PRIVATE_METHODS

	private IEnumerator Start()
	{
		if (string.IsNullOrEmpty(savePath))
			savePath = System.IO.Path.GetFullPath(UnityEngine.Application.dataPath + "/../Network Statistics Plots/" +
				System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff") + "/");
		m_statisticsList = new List<NetworkStatistics>();
		while (true)
		{
			yield return new WaitForSeconds(saveFrequency);
			var stats = new NetworkStatistics();
			stats.CalculateNetworkStats();
			m_statisticsList.Add(stats);
		}
	}

	private void OnApplicationQuit()
	{
		DrawStatisticsPlots();
		Debug.Log("Statistics Plots Saved to " + savePath);
	}

	private void DrawStatisticsPlots()
	{
		var timeAxis = new List<double>();
		timeAxis.AddRange(m_statisticsList.Select(c => (double)c.time));
		DrawPlot(m_statisticsList, (c) => (double)c.connectionsCount, timeAxis.ToArray(),
			"ConnectionsCount over Time", "Time", "ConnectionsCount", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.bytesReceived, timeAxis.ToArray(),
			"BytesReceived over Time", "Time", "BytesReceived", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.bytesReceivedPerSecond, timeAxis.ToArray(),
			"BytesReceivedPerSecond over Time", "Time", "BytesReceivedPerSecond", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.bytesSent, timeAxis.ToArray(),
			"BytesReceived over Time", "Time", "BytesSent", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.bytesSentPerSecond, timeAxis.ToArray(),
			"BytesSentPerSecond over Time", "Time", "BytesSentPerSecond", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.messageDuplicatesRejected, timeAxis.ToArray(),
			"MessageDuplicatesRejected over Time", "Time", "MessageDuplicatesRejected", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.messageSequencesRejected, timeAxis.ToArray(),
			"MessageSequencesRejected over Time", "Time", "MessageSequencesRejected", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.messagesReceived, timeAxis.ToArray(),
			"MessagesReceived over Time", "Time", "MessagesReceived", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.messagesResent, timeAxis.ToArray(),
			"MessagesResent over Time", "Time", "MessagesResent", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.messagesSent, timeAxis.ToArray(),
			"MessagesSent over Time", "Time", "MessagesSent", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.messagesStored, timeAxis.ToArray(),
			"MessagesStored over Time", "Time", "MessagesStored", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.messagesUnsent, timeAxis.ToArray(),
			"MessagesUnsent over Time", "Time", "MessagesUnsent", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.messagesWithheld, timeAxis.ToArray(),
			"MessagesWithheld over Time", "Time", "MessagesWithheld", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.packetsReceived, timeAxis.ToArray(),
			"PacketsReceived over Time", "Time", "PacketsReceived", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.packetsSent, timeAxis.ToArray(),
			"PacketsSent over Time", "Time", "PacketsSent", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.userBytesReceived, timeAxis.ToArray(),
			"UserBytesReceived over Time", "Time", "UserBytesReceived", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.userBytesReceivedPerSecond, timeAxis.ToArray(),
			"UserBytesReceivedPerSecond over Time", "Time", "UserBytesReceivedPerSecond", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.userBytesSent, timeAxis.ToArray(),
			"UserBytesSent over Time", "Time", "UserBytesSent", lineColor, savePath);
		DrawPlot(m_statisticsList, (c) => (double)c.userBytesSentPerSecond, timeAxis.ToArray(),
			"UserBytesSentPerSecond over Time", "Time", "UserBytesSentPerSecond", lineColor, savePath);
	}

	private void DrawPlot(List<NetworkStatistics> stats, Func<NetworkStatistics, double> selectMethod,
		double[] xAxisData, string plotName, string xAxisLabel, string yAxisLabel, Color lineColor, string savePath)
	{
		var yAxisdata = new List<double>();
		yAxisdata.AddRange(stats.Select(selectMethod));
		var plots = new PlotData("ServerStatistics", xAxisData, yAxisdata.ToArray(), lineColor);
		uPlot.DrawPlot(plotName, plots, xAxisLabel, yAxisLabel, savePath, backColor, plotBackColor, legendBackColor);
	}

	#endregion PRIVATE_METHODS
}