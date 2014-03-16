﻿Shader "Shaders/Test"
{
    Properties
    {
        _Color ("Color Tint", Color) = (1,1,1,1)
        _MainTex ("Main Tex", 2D) = "white"
    }
 
    Category
    {
    	// Lighting Off
    	// ZWrite Off
    	// Cull back
        Blend SrcAlpha OneMinusSrcAlpha
        
      
               
        Tags {Queue=Transparent}
 
        SubShader
        {
        	Pass
            {
            	SetTexture [_MainTex]
            		{
                    	ConstantColor [_Color]
                   		Combine Texture * constant
                	}
            }
        }
        
     
    }
}
