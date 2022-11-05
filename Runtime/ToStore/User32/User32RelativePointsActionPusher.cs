public class User32RelativePointsActionPusher
{

    private readonly static object threadLock = new object();
    public delegate void MouseActionBasedOnRelativePoints(IntPtrWrapGet pointer, User32RelativePixelPointLRTB point);
    public delegate void MouseActionBasedOnRelativeKey(IntPtrWrapGet pointer);
    public static void PointsListOfPressReleaseActions(
        IntPtrWrapGet pointer,
        MouseActionBasedOnRelativeKey toDoAtTheStart,
        MouseActionBasedOnRelativePoints actionDown,
        MouseActionBasedOnRelativePoints actionUp,
        MouseActionBasedOnRelativePoints moveTo,
        MouseActionBasedOnRelativeKey toDoAtTheEnd,
        User32RelativePixelPointLRTB[] points,
        out int msCountAtEnd,
        int forgroundMsWait = 150,
        int betweenActionMsWait = 90,
        int pressActionMsWait = 0,
        int tempTimeBeforeStartMs = 0,
        int previsionMoveAfter = 30,
        bool useForgroundAtStart = true
        )
    {
        lock (threadLock)
        {
            int ms = tempTimeBeforeStartMs;
            if (useForgroundAtStart)
            {
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                    WindowIntPtrUtility.SetForegroundWindow(pointer);
                });

                ms += forgroundMsWait;
            }

            if (toDoAtTheStart != null)
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                    toDoAtTheStart(pointer);
                });
            for (int i = 0; i < points.Length; i++)
            {
                User32RelativePixelPointLRTB p = new User32RelativePixelPointLRTB(points[i].m_pixelLeft2Right, points[i].m_pixelTop2Bot);

                if (i == 0)
                {
                    ms += (previsionMoveAfter);
                    ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                        if (moveTo != null)
                            moveTo(pointer, p);
                    });
                    ms += (previsionMoveAfter);
                }

                ms += betweenActionMsWait;
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                    if (actionDown != null)
                        actionDown(pointer, p);
                });
                ms += pressActionMsWait;
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                    if (actionUp != null)
                        actionUp(pointer, p);
                });
                ms += (previsionMoveAfter);
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {
                    if (moveTo != null)
                        moveTo(pointer, p);
                });
                ms += (previsionMoveAfter);


            }
            ms += betweenActionMsWait;
            if (toDoAtTheEnd != null)
                ThreadQueueDateTimeCall.Instance.AddFromNowInMs(ms, () => {

                    toDoAtTheEnd(pointer);
                });
            msCountAtEnd = ms;
        }

    }
}
