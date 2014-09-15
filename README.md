uPlot
=====

uPlot is a simple class to draw plots of a range of values in a file. It can be used for saving performance and network related data for later comparison and any other task which requires charting related capabilities.

uPlot used a library called NPlot for drawing graphs. We only use it's line charts but it has other kinds of charts to be used. 
http://netcontrols.org/nplot/wiki/


uPlot.cs file is a class which you can use to draw plots in bitmap files in general.
StatisticsPlot.cs is dependent on uLink. You can attach it to a gameObject in your game server and in OnApplicationQuit it will draw graphs for all uLink.NetworkStatistics
