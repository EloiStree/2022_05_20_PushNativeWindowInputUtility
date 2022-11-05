[System.Serializable]
public struct User32RelativePixelPointLRTB
{
    public int m_pixelLeft2Right;
    public int m_pixelTop2Bot;

    public User32RelativePixelPointLRTB(int pixelLeft2Right, int pixelTop2Bot)
    {
        m_pixelLeft2Right = pixelLeft2Right;
        m_pixelTop2Bot = pixelTop2Bot;
    }
}