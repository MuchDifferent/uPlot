uPlot
=====

uPlot is a simple class to draw plots of a range of values in a file. It can be used for saving performance and network related data for later comparison and any other task which requires charting related capabilities.

uPlot uses a library called NPlot for drawing graphs. We only use it's line charts but it has other kinds of charts to be used. 
http://netcontrols.org/nplot/wiki/


uPlot.cs file is a class which you can use to draw plots in bitmap files in general.
StatisticsPlot.cs is the component which you should attach to a gameObject in your server scene and at quit time it will draw everything for you in the path specified.

If you only need plotting functionality either use uPlot or NPlot directly, none of them require uLink but all files other than NPlot directory's content and uPlot.cs require uLink to function.'

You need to set API compatibility level to .NET 2.0 and not .NET 2 subset. The library uses windows forms related assemblies which don't work in that mode.'