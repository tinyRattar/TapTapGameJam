using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Setting
{
    public static class UISortingOrder
    {
        public const int LAYER0 = 0;
        public const int LAYER1 = 1;
        public const int LAYER2 = 2;
    }

    public static class Path
    {
        public const string PR_Element = "Element";
        public const string SP_ElementEarth = "earth";
        public const string SP_ElementFire = "fire";
        public const string SP_ElementWater = "water";
        public const string SP_ElementMetal = "metal";
        public const string SP_ElementWood = "wood";

        public const string PR_Diamond = "Diamond";
        public const string SP_DEarth = "d_earth";
        public const string SP_DFire = "d_fire";
        public const string SP_DWater = "d_water";
        public const string SP_DMetal = "d_metal";
        public const string SP_DWood = "d_wood";

        public const string SHADER_Dissolve = "Shader/Dissolve";
        public const string MAT_Dissolve = "ShaderAndMat/DissolveMat";
    }
}
