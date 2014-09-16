#region USINGS

using NPlot;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#endregion USINGS

/// <summary>
/// This class can be used for drawing plots in bitmap files.
/// </summary>
/// <remarks>
/// There are a set of constant values in the class which is not received by methods due to being constant most of the times.
/// Feel free to modify them according to your needs.
/// </remarks>
public class uPlot
{
	#region PRIVATE_VARIABLES

	/// <summary>
	/// Width of the bitmap file.
	/// </summary>
	private static readonly int width = 800;

	/// <summary>
	/// Height of the bitmap file.
	/// </summary>
	private static readonly int height = 600;

	#endregion PRIVATE_VARIABLES

	#region PUBLIC_METHODS

	/// <summary>
	/// Draws a plot and saves it to a bitmap file.
	/// </summary>
	/// <param name="plotName">Name of the whole plot which will be written on top of the image as well</param>
	/// <param name="plotData">The <see cref="PlotData"/> which will be used to draw the plot</param>
	/// <param name="xAxisLabel">Name of the label for x axis</param>
	/// <param name="yAxisLabel">Name of the label for y axis</param>
	/// <param name="savePath">The path that the bitmap file will be saved in it</param>
	/// <param name="backColor">BackGround color of the whole image behind the plot rectangle</param>
	/// <param name="plotBackColor">Background color of the plot behind plot lines inside the plot rectangle</param>
	/// <param name="legendBackColor">Background color of the plot's legend</param>
	public static void DrawPlot(string plotName, PlotData plotData,
		string xAxisLabel, string yAxisLabel, string savePath, Color backColor, Color plotBackColor, Color legendBackColor)
	{
		NPlot.Bitmap.PlotSurface2D surface = CreateSurface(width, height, plotName,
			xAxisLabel, yAxisLabel, backColor, plotBackColor, legendBackColor);

		var linePlot = CreateLinePlot(plotData.xAxisData, plotData.yAxisData,
			new System.Drawing.Pen(ConvertToSystemColor(plotData.lineColor)), plotData.name);

		surface.Add(linePlot);
		surface.Refresh();
		SaveSurface(surface, savePath, plotName);
	}

	/// <summary>
	/// Draws a plot consisting of multiple lines for different values on a single plot.
	/// </summary>
	/// <param name="plotName">Name of the whole plot which will be written on top of the image as well</param>
	/// <param name="plotsData">The array of data series which the line charts will be created based on them</param>
	/// <param name="xAxisLabel">Name of the label for x axis</param>
	/// <param name="yAxisLabel">Name of the label for y axis</param>
	/// <param name="savePath">The path that the image file will be saved in it</param>
	/// <param name="backColor">BackGround color of the whole image behind the plot rectangle</param>
	/// <param name="plotBackColor">Background color of the plot behind plot lines inside the plot rectangle</param>
	/// <param name="legendBackColor">Background color of the plot's legend</param>
	public static void DrawPlot(string plotName, List<PlotData> plotsData,
		string xAxisLabel, string yAxisLabel, string savePath, Color backColor, Color plotBackColor, Color legendBackColor)
	{
		NPlot.Bitmap.PlotSurface2D surface = CreateSurface(width, height, plotName,
			xAxisLabel, yAxisLabel, backColor, plotBackColor, legendBackColor);

		for (int i = 0; i < plotsData.Count; ++i)
		{
			var linePlot = CreateLinePlot(plotsData[i].xAxisData, plotsData[i].yAxisData,
				new System.Drawing.Pen(ConvertToSystemColor(plotsData[i].lineColor)), plotsData[i].name);
			surface.Add(linePlot);
		}

		surface.Refresh();
		SaveSurface(surface, savePath, plotName);
	}

	#endregion PUBLIC_METHODS

	#region PRIVATE_METHODS

	private static NPlot.Bitmap.PlotSurface2D CreateSurface(int width, int height,
		string title, string xAxisLabel, string yAxisLabel, Color backColor, Color plotBackColor, Color LegendBackColor)
	{
		NPlot.Bitmap.PlotSurface2D surface = new NPlot.Bitmap.PlotSurface2D(width, height);
		surface.Title = title;
		surface.Legend = new NPlot.Legend();
		surface.Legend.BackgroundColor = ConvertToSystemColor(LegendBackColor);
		surface.XAxis1 = new LinearAxis();
		surface.YAxis1 = new LinearAxis();
		surface.XAxis1.Label = xAxisLabel;
		surface.YAxis1.Label = yAxisLabel;
		surface.PlotBackColor = ConvertToSystemColor(plotBackColor);
		surface.BackColor = ConvertToSystemColor(backColor);
		surface.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
		return surface;
	}

	private static NPlot.LinePlot CreateLinePlot(double[] xAxis, double[] yAxis, System.Drawing.Pen pen, string name)
	{
		NPlot.LinePlot lp = new NPlot.LinePlot(yAxis, xAxis);
		lp.Pen = pen;
		lp.Label = name;
		lp.Shadow = true;
		return lp;
	}

	private static void SaveSurface(NPlot.Bitmap.PlotSurface2D surface, string path, string plotName)
	{
		if (!Directory.Exists(path))
			(new System.IO.FileInfo(path)).Directory.Create();
		surface.Bitmap.Save(path + plotName + ".bmp");
	}

	private static System.Drawing.Color ConvertToSystemColor(Color color)
	{
		return System.Drawing.Color.FromArgb(
			ConvertColorRange(color.a),
			ConvertColorRange(color.r),
			ConvertColorRange(color.g),
			ConvertColorRange(color.b));
	}

	private static int ConvertColorRange(float f)
	{
		return Mathf.FloorToInt(Mathf.Clamp(f * 255f, 0, 255));
	}

	#endregion PRIVATE_METHODS
}