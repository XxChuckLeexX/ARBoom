Shader "Custom/Mask"
{
    SubShader
    {
        Tags
        {
            "Queue" = "Geometry-10"
        }

        Lighting off
        ZTest LEqual
        ZWrite On
        ColorMask 0
        Pass
        {
        }
    }
}