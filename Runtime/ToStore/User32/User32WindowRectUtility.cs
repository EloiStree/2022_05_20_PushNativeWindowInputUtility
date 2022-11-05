using Eloi;
using UnityEngine;

public class User32WindowRectUtility
{

    public static void RefreshInfoOf(ref DeductedInfoOfWindowSizeWithSource rectInfo)
    {
        WindowIntPtrUtility.RefreshInfoOf(ref rectInfo);
    }

    public static void HorizontalLineLeftRightClick(
        in DeductedInfoOfWindowSizeWithSource rectInfo,
        in Bot2TopPercent01 marginDown,
        in Left2RightPercent01 marginLeft,
        in Right2LeftPercent01 marginRight,
        int pointCount,
        out User32RelativePixelPointLRTB[] points
        )
    {
        points = new User32RelativePixelPointLRTB[pointCount];
        rectInfo.m_frameSize.GetBottomToTopToRelative(marginDown.GetPercent(), out int y);
        rectInfo.m_frameSize.GetLeftToRightToRelative((marginLeft.GetPercent()), out int xl);
        rectInfo.m_frameSize.GetRightToLeftToRelative((marginRight.GetPercent()), out int xr);
        for (int i = 0; i < pointCount; i++)
        {
            float percent = i / (float)pointCount;
            points[i] = new User32RelativePixelPointLRTB((int)Mathf.Lerp(xl, xr, percent), y);
        }
    }
    public static void VerticalLineBotTopClick(in DeductedInfoOfWindowSizeWithSource rectInfo, in Left2RightPercent01 marginLeft, in Bot2TopPercent01 marginDown, in Right2LeftPercent01 marginTop)
    {
        Eloi.E_ThrowException.ThrowNotImplemented();
    }

    public enum HeightMeasure { BasedOnWidth, BasedOnHeight, BasedOnMagnitude }
    public static void RandomSphereClickAroundPoint(in DeductedInfoOfWindowSizeWithSource rectInfo,
        in Left2RightPercent01 marginLeft,
        in Bot2TopPercent01 marginDown,
        in Percent01 screenPercent,
        HeightMeasure heightMeasure,
        int pointCount,
        out User32RelativePixelPointLRTB[] points)
    {
        rectInfo.m_frameSize.GetBottomToTopToRelative(marginDown.GetPercent(), out int y);
        rectInfo.m_frameSize.GetLeftToRightToRelative((marginLeft.GetPercent()), out int x);
        int radiusInPixel = 1;
        if (heightMeasure == HeightMeasure.BasedOnWidth)
            radiusInPixel = (int)(rectInfo.m_frameSize.m_innerWidth * screenPercent.GetPercent());
        if (heightMeasure == HeightMeasure.BasedOnHeight)
            radiusInPixel = (int)(rectInfo.m_frameSize.m_innerHeight * screenPercent.GetPercent());

        points = new User32RelativePixelPointLRTB[pointCount];
        for (int i = 0; i < points.Length; i++)
        {
            Eloi.E_UnityRandomUtility.GetRandomN2M(-radiusInPixel, radiusInPixel, out int rx);
            Eloi.E_UnityRandomUtility.GetRandomN2M(-radiusInPixel, radiusInPixel, out int ry);
            points[i].m_pixelLeft2Right = x + rx;
            points[i].m_pixelTop2Bot = y + ry;
        }
    }
}
