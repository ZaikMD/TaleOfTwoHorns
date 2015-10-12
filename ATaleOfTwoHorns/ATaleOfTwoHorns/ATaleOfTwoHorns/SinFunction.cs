using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//created by Kris Matis

namespace ATaleOfTwoHorns
{
    class SinFunction
    {        
        float m_Amplitude;
        float m_WaveLength;
        float m_Period;
        float m_VerticleTranslation;

        public SinFunction(float amplitude = 10.0f, float waveLength = 0.08f,
                           float period = 0.0f, float verticleTranslation = +100.0f)
        {
            m_Amplitude = amplitude;
            m_WaveLength = waveLength;
            m_Period = period;
            m_VerticleTranslation = verticleTranslation;
        }

        public float getYAtPosX(float x)
        {
            return (float)(m_Amplitude * (Math.Sin((double)((m_WaveLength * x) + m_Period))) + m_VerticleTranslation);
        }

        public static float degreesToRadians(float degrees)
        {
            return degrees / 57.2957795f;
        }

        public static float radiansToDegrees(float radians)
        {
            return radians * 57.2957795f;
        }
    }
}
