using System;

using System.Runtime.InteropServices;

namespace BLITTEngine.Foundation
{
    internal enum EnableCap : uint
    {
        PointSmooth = 2832,
        LineSmooth = 2848,
        LineStipple = 2852,
        PolygonSmooth = 2881,
        PolygonStipple = 2882,
        CullFace = 2884,
        Lighting = 2896,
        ColorMaterial = 2903,
        Fog = 2912,
        DepthTest = 2929,
        StencilTest = 2960,
        Normalize = 2977,
        AlphaTest = 3008,
        Dither = 3024,
        Blend = 3042,
        IndexLogicOp = 3057,
        ColorLogicOp = 3058,
        ScissorTest = 3089,
        TextureGenS = 3168,
        TextureGenT = 3169,
        TextureGenR = 3170,
        TextureGenQ = 3171,
        AutoNormal = 3456,
        Map1Color4 = 3472,
        Map1Index = 3473,
        Map1Normal = 3474,
        Map1TextureCoord1 = 3475,
        Map1TextureCoord2 = 3476,
        Map1TextureCoord3 = 3477,
        Map1TextureCoord4 = 3478,
        Map1Vertex3 = 3479,
        Map1Vertex4 = 3480,
        Map2Color4 = 3504,
        Map2Index = 3505,
        Map2Normal = 3506,
        Map2TextureCoord1 = 3507,
        Map2TextureCoord2 = 3508,
        Map2TextureCoord3 = 3509,
        Map2TextureCoord4 = 3510,
        Map2Vertex3 = 3511,
        Map2Vertex4 = 3512,
        Texture1D = 3552,
        Texture2D = 3553,
        PolygonOffsetPoint = 10753,
        PolygonOffsetLine = 10754,
        ClipDistance0 = 12288,
        ClipPlane0 = 12288,
        ClipDistance1 = 12289,
        ClipPlane1 = 12289,
        ClipDistance2 = 12290,
        ClipPlane2 = 12290,
        ClipDistance3 = 12291,
        ClipPlane3 = 12291,
        ClipDistance4 = 12292,
        ClipPlane4 = 12292,
        ClipDistance5 = 12293,
        ClipPlane5 = 12293,
        ClipDistance6 = 12294,
        ClipDistance7 = 12295,
        Light0 = 16384,
        Light1 = 16385,
        Light2 = 16386,
        Light3 = 16387,
        Light4 = 16388,
        Light5 = 16389,
        Light6 = 16390,
        Light7 = 16391,
        Convolution1D = 32784,
        Convolution1DExt = 32784,
        Convolution2D = 32785,
        Convolution2DExt = 32785,
        Separable2D = 32786,
        Separable2DExt = 32786,
        Histogram = 32804,
        HistogramExt = 32804,
        MinmaxExt = 32814,
        PolygonOffsetFill = 32823,
        RescaleNormal = 32826,
        RescaleNormalExt = 32826,
        Texture3DExt = 32879,
        VertexArray = 32884,
        NormalArray = 32885,
        ColorArray = 32886,
        IndexArray = 32887,
        TextureCoordArray = 32888,
        EdgeFlagArray = 32889,
        InterlaceSgix = 32916,
        Multisample = 32925,
        MultisampleSgis = 32925,
        SampleAlphaToCoverage = 32926,
        SampleAlphaToMaskSgis = 32926,
        SampleAlphaToOne = 32927,
        SampleAlphaToOneSgis = 32927,
        SampleCoverage = 32928,
        SampleMaskSgis = 32928,
        TextureColorTableSgi = 32956,
        ColorTable = 32976,
        ColorTableSgi = 32976,
        PostConvolutionColorTable = 32977,
        PostConvolutionColorTableSgi = 32977,
        PostColorMatrixColorTable = 32978,
        PostColorMatrixColorTableSgi = 32978,
        Texture4DSgis = 33076,
        PixelTexGenSgix = 33081,
        SpriteSgix = 33096,
        ReferencePlaneSgix = 33149,
        IrInstrument1Sgix = 33151,
        CalligraphicFragmentSgix = 33155,
        FramezoomSgix = 33163,
        FogOffsetSgix = 33176,
        SharedTexturePaletteExt = 33275,
        DebugOutputSynchronous = 33346,
        AsyncHistogramSgix = 33580,
        PixelTextureSgis = 33619,
        AsyncTexImageSgix = 33628,
        AsyncDrawPixelsSgix = 33629,
        AsyncReadPixelsSgix = 33630,
        FragmentLightingSgix = 33792,
        FragmentColorMaterialSgix = 33793,
        FragmentLight0Sgix = 33804,
        FragmentLight1Sgix = 33805,
        FragmentLight2Sgix = 33806,
        FragmentLight3Sgix = 33807,
        FragmentLight4Sgix = 33808,
        FragmentLight5Sgix = 33809,
        FragmentLight6Sgix = 33810,
        FragmentLight7Sgix = 33811,
        FogCoordArray = 33879,
        ColorSum = 33880,
        SecondaryColorArray = 33886,
        TextureRectangle = 34037,
        TextureCubeMap = 34067,
        ProgramPointSize = 34370,
        VertexProgramPointSize = 34370,
        VertexProgramTwoSide = 34371,
        DepthClamp = 34383,
        TextureCubeMapSeamless = 34895,
        PointSprite = 34913,
        SampleShading = 35894,
        RasterizerDiscard = 35977,
        PrimitiveRestartFixedIndex = 36201,
        FramebufferSrgb = 36281,
        SampleMask = 36433,
        PrimitiveRestart = 36765,
        DebugOutput = 37600
    }
    
    internal enum TextureTarget : uint
    {
        Texture1D = 3552,
        Texture2D = 3553,
        ProxyTexture1D = 32867,
        ProxyTexture1DExt = 32867,
        ProxyTexture2D = 32868,
        ProxyTexture2DExt = 32868,
        Texture3D = 32879,
        Texture3DExt = 32879,
        Texture3DOes = 32879,
        ProxyTexture3D = 32880,
        ProxyTexture3DExt = 32880,
        DetailTexture2DSgis = 32917,
        Texture4DSgis = 33076,
        ProxyTexture4DSgis = 33077,
        TextureMinLod = 33082,
        TextureMinLodSgis = 33082,
        TextureMaxLod = 33083,
        TextureMaxLodSgis = 33083,
        TextureBaseLevel = 33084,
        TextureBaseLevelSgis = 33084,
        TextureMaxLevel = 33085,
        TextureMaxLevelSgis = 33085,
        TextureRectangle = 34037,
        TextureRectangleArb = 34037,
        TextureRectangleNv = 34037,
        ProxyTextureRectangle = 34039,
        TextureCubeMap = 34067,
        TextureBindingCubeMap = 34068,
        TextureCubeMapPositiveX = 34069,
        TextureCubeMapNegativeX = 34070,
        TextureCubeMapPositiveY = 34071,
        TextureCubeMapNegativeY = 34072,
        TextureCubeMapPositiveZ = 34073,
        TextureCubeMapNegativeZ = 34074,
        ProxyTextureCubeMap = 34075,
        Texture1DArray = 35864,
        ProxyTexture1DArray = 35865,
        Texture2DArray = 35866,
        ProxyTexture2DArray = 35867,
        TextureBuffer = 35882,
        TextureCubeMapArray = 36873,
        ProxyTextureCubeMapArray = 36875,
        Texture2DMultisample = 37120,
        ProxyTexture2DMultisample = 37121,
        Texture2DMultisampleArray = 37122,
        ProxyTexture2DMultisampleArray = 37123
    }
    
    public enum PixelInternalFormat : int
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        DepthComponent = 6402,
        Alpha = 6406,
        Rgb = 6407,
        Rgba = 6408,
        Luminance = 6409,
        LuminanceAlpha = 6410,
        R3G3B2 = 10768,
        Alpha4 = 32827,
        Alpha8 = 32828,
        Alpha12 = 32829,
        Alpha16 = 32830,
        Luminance4 = 32831,
        Luminance8 = 32832,
        Luminance12 = 32833,
        Luminance16 = 32834,
        Luminance4Alpha4 = 32835,
        Luminance6Alpha2 = 32836,
        Luminance8Alpha8 = 32837,
        Luminance12Alpha4 = 32838,
        Luminance12Alpha12 = 32839,
        Luminance16Alpha16 = 32840,
        Intensity = 32841,
        Intensity4 = 32842,
        Intensity8 = 32843,
        Intensity12 = 32844,
        Intensity16 = 32845,
        Rgb2Ext = 32846,
        Rgb4 = 32847,
        Rgb5 = 32848,
        Rgb8 = 32849,
        Rgb10 = 32850,
        Rgb12 = 32851,
        Rgb16 = 32852,
        Rgba2 = 32853,
        Rgba4 = 32854,
        Rgb5A1 = 32855,
        Rgba8 = 32856,
        Rgb10A2 = 32857,
        Rgba12 = 32858,
        Rgba16 = 32859,
        DualAlpha4Sgis = 33040,
        DualAlpha8Sgis = 33041,
        DualAlpha12Sgis = 33042,
        DualAlpha16Sgis = 33043,
        DualLuminance4Sgis = 33044,
        DualLuminance8Sgis = 33045,
        DualLuminance12Sgis = 33046,
        DualLuminance16Sgis = 33047,
        DualIntensity4Sgis = 33048,
        DualIntensity8Sgis = 33049,
        DualIntensity12Sgis = 33050,
        DualIntensity16Sgis = 33051,
        DualLuminanceAlpha4Sgis = 33052,
        DualLuminanceAlpha8Sgis = 33053,
        QuadAlpha4Sgis = 33054,
        QuadAlpha8Sgis = 33055,
        QuadLuminance4Sgis = 33056,
        QuadLuminance8Sgis = 33057,
        QuadIntensity4Sgis = 33058,
        QuadIntensity8Sgis = 33059,
        DepthComponent16 = 33189,
        DepthComponent16Sgix = 33189,
        DepthComponent24 = 33190,
        DepthComponent24Sgix = 33190,
        DepthComponent32 = 33191,
        DepthComponent32Sgix = 33191,
        CompressedRed = 33317,
        CompressedRg = 33318,
        R8 = 33321,
        R16 = 33322,
        Rg8 = 33323,
        Rg16 = 33324,
        R16f = 33325,
        R32f = 33326,
        Rg16f = 33327,
        Rg32f = 33328,
        R8i = 33329,
        R8ui = 33330,
        R16i = 33331,
        R16ui = 33332,
        R32i = 33333,
        R32ui = 33334,
        Rg8i = 33335,
        Rg8ui = 33336,
        Rg16i = 33337,
        Rg16ui = 33338,
        Rg32i = 33339,
        Rg32ui = 33340,
        CompressedRgbS3tcDxt1Ext = 33776,
        CompressedRgbaS3tcDxt1Ext = 33777,
        CompressedRgbaS3tcDxt3Ext = 33778,
        CompressedRgbaS3tcDxt5Ext = 33779,
        RgbIccSgix = 33888,
        RgbaIccSgix = 33889,
        AlphaIccSgix = 33890,
        LuminanceIccSgix = 33891,
        IntensityIccSgix = 33892,
        LuminanceAlphaIccSgix = 33893,
        R5G6B5IccSgix = 33894,
        R5G6B5A8IccSgix = 33895,
        Alpha16IccSgix = 33896,
        Luminance16IccSgix = 33897,
        Intensity16IccSgix = 33898,
        Luminance16Alpha8IccSgix = 33899,
        CompressedAlpha = 34025,
        CompressedLuminance = 34026,
        CompressedLuminanceAlpha = 34027,
        CompressedIntensity = 34028,
        CompressedRgb = 34029,
        CompressedRgba = 34030,
        DepthStencil = 34041,
        Rgba32f = 34836,
        Rgb32f = 34837,
        Rgba16f = 34842,
        Rgb16f = 34843,
        Depth24Stencil8 = 35056,
        R11fG11fB10f = 35898,
        Rgb9E5 = 35901,
        Srgb = 35904,
        Srgb8 = 35905,
        SrgbAlpha = 35906,
        Srgb8Alpha8 = 35907,
        SluminanceAlpha = 35908,
        Sluminance8Alpha8 = 35909,
        Sluminance = 35910,
        Sluminance8 = 35911,
        CompressedSrgb = 35912,
        CompressedSrgbAlpha = 35913,
        CompressedSluminance = 35914,
        CompressedSluminanceAlpha = 35915,
        CompressedSrgbS3tcDxt1Ext = 35916,
        CompressedSrgbAlphaS3tcDxt1Ext = 35917,
        CompressedSrgbAlphaS3tcDxt3Ext = 35918,
        CompressedSrgbAlphaS3tcDxt5Ext = 35919,
        DepthComponent32f = 36012,
        Depth32fStencil8 = 36013,
        Rgba32ui = 36208,
        Rgb32ui = 36209,
        Rgba16ui = 36214,
        Rgb16ui = 36215,
        Rgba8ui = 36220,
        Rgb8ui = 36221,
        Rgba32i = 36226,
        Rgb32i = 36227,
        Rgba16i = 36232,
        Rgb16i = 36233,
        Rgba8i = 36238,
        Rgb8i = 36239,
        Float32UnsignedInt248Rev = 36269,
        CompressedRedRgtc1 = 36283,
        CompressedSignedRedRgtc1 = 36284,
        CompressedRgRgtc2 = 36285,
        CompressedSignedRgRgtc2 = 36286,
        CompressedRgbaBptcUnorm = 36492,
        CompressedSrgbAlphaBptcUnorm = 36493,
        CompressedRgbBptcSignedFloat = 36494,
        CompressedRgbBptcUnsignedFloat = 36495,
        R8Snorm = 36756,
        Rg8Snorm = 36757,
        Rgb8Snorm = 36758,
        Rgba8Snorm = 36759,
        R16Snorm = 36760,
        Rg16Snorm = 36761,
        Rgb16Snorm = 36762,
        Rgba16Snorm = 36763,
        Rgb10A2ui = 36975,
        CompressedRgb8Etc2 = 0x9274,
        CompressedRgb8PunchthroughAlpha1Etc2 = 0x9276,
        CompressedRgba8Etc2Eac = 0x9278,
    }
    
    public enum GLPixelFormat : uint
    {
        UnsignedShort = 5123,
        UnsignedInt = 5125,
        ColorIndex = 6400,
        StencilIndex = 6401,
        DepthComponent = 6402,
        Red = 6403,
        RedExt = 6403,
        Green = 6404,
        Blue = 6405,
        Alpha = 6406,
        Rgb = 6407,
        Rgba = 6408,
        Luminance = 6409,
        LuminanceAlpha = 6410,
        AbgrExt = 32768,
        CmykExt = 32780,
        CmykaExt = 32781,
        Bgr = 32992,
        Bgra = 32993,
        Ycrcb422Sgix = 33211,
        Ycrcb444Sgix = 33212,
        Rg = 33319,
        RgInteger = 33320,
        R5G6B5IccSgix = 33894,
        R5G6B5A8IccSgix = 33895,
        Alpha16IccSgix = 33896,
        Luminance16IccSgix = 33897,
        Luminance16Alpha8IccSgix = 33899,
        DepthStencil = 34041,
        RedInteger = 36244,
        GreenInteger = 36245,
        BlueInteger = 36246,
        AlphaInteger = 36247,
        RgbInteger = 36248,
        RgbaInteger = 36249,
        BgrInteger = 36250,
        BgraInteger = 36251
    }

    public enum GLPixelType : uint
    {
        Byte = 5120,
        UnsignedByte = 5121,
        Short = 5122,
        UnsignedShort = 5123,
        Int = 5124,
        UnsignedInt = 5125,
        Float = 5126,
        HalfFloat = 5131,
        Bitmap = 6656,
        UnsignedByte332 = 32818,
        UnsignedByte332Ext = 32818,
        UnsignedShort4444 = 32819,
        UnsignedShort4444Ext = 32819,
        UnsignedShort5551 = 32820,
        UnsignedShort5551Ext = 32820,
        UnsignedInt8888 = 32821,
        UnsignedInt8888Ext = 32821,
        UnsignedInt1010102 = 32822,
        UnsignedInt1010102Ext = 32822,
        UnsignedByte233Reversed = 33634,
        UnsignedShort565 = 33635,
        UnsignedShort565Reversed = 33636,
        UnsignedShort4444Reversed = 33637,
        UnsignedShort1555Reversed = 33638,
        UnsignedInt8888Reversed = 33639,
        UnsignedInt2101010Reversed = 33640,
        UnsignedInt248 = 34042,
        UnsignedInt10F11F11FRev = 35899,
        UnsignedInt5999Rev = 35902,
        Float32UnsignedInt248Rev = 36269
    }
    
    public enum BlendingFactorSrc : uint
    {
        Zero = 0,
        One = 1,
        SrcColor = 768,
        OneMinusSrcColor = 769,
        SrcAlpha = 770,
        OneMinusSrcAlpha = 771,
        DstAlpha = 772,
        OneMinusDstAlpha = 773,
        DstColor = 774,
        OneMinusDstColor = 775,
        SrcAlphaSaturate = 776,
        ConstantColor = 32769,
        ConstantColorExt = 32769,
        OneMinusConstantColor = 32770,
        OneMinusConstantColorExt = 32770,
        ConstantAlpha = 32771,
        ConstantAlphaExt = 32771,
        OneMinusConstantAlpha = 32772,
        OneMinusConstantAlphaExt = 32772,
        Src1Alpha = 34185,
        Src1Color = 35065,
        OneMinusSrc1Color = 35066,
        OneMinusSrc1Alpha = 35067
    }

    public enum BlendingFactorDest : uint
    {
        Zero = 0,
        One = 1,
        SrcColor = 768,
        OneMinusSrcColor = 769,
        SrcAlpha = 770,
        OneMinusSrcAlpha = 771,
        DstAlpha = 772,
        OneMinusDstAlpha = 773,
        DstColor = 774,
        OneMinusDstColor = 775,
        SrcAlphaSaturate = 776,
        ConstantColor = 32769,
        ConstantColorExt = 32769,
        OneMinusConstantColor = 32770,
        OneMinusConstantColorExt = 32770,
        ConstantAlpha = 32771,
        ConstantAlphaExt = 32771,
        OneMinusConstantAlpha = 32772,
        OneMinusConstantAlphaExt = 32772,
        Src1Alpha = 34185,
        Src1Color = 35065,
        OneMinusSrc1Color = 35066,
        OneMinusSrc1Alpha = 35067
    }
    
    public enum TextureParameterName : uint
    {
        TextureBorderColor = 4100,
        TextureMagFilter = 10240,
        TextureMinFilter = 10241,
        TextureWrapS = 10242,
        TextureWrapT = 10243,
        TexturePriority = 32870,
        TexturePriorityExt = 32870,
        TextureDepth = 32881,
        TextureWrapR = 32882,
        TextureWrapRExt = 32882,
        TextureWrapROes = 32882,
        DetailTextureLevelSgis = 32922,
        DetailTextureModeSgis = 32923,
        ShadowAmbientSgix = 32959,
        TextureCompareFailValue = 32959,
        DualTextureSelectSgis = 33060,
        QuadTextureSelectSgis = 33061,
        ClampToBorder = 33069,
        ClampToEdge = 33071,
        TextureWrapQSgis = 33079,
        TextureMinLod = 33082,
        TextureMaxLod = 33083,
        TextureBaseLevel = 33084,
        TextureMaxLevel = 33085,
        TextureClipmapCenterSgix = 33137,
        TextureClipmapFrameSgix = 33138,
        TextureClipmapOffsetSgix = 33139,
        TextureClipmapVirtualDepthSgix = 33140,
        TextureClipmapLodOffsetSgix = 33141,
        TextureClipmapDepthSgix = 33142,
        PostTextureFilterBiasSgix = 33145,
        PostTextureFilterScaleSgix = 33146,
        TextureLodBiasSSgix = 33166,
        TextureLodBiasTSgix = 33167,
        TextureLodBiasRSgix = 33168,
        GenerateMipmap = 33169,
        GenerateMipmapSgis = 33169,
        TextureCompareSgix = 33178,
        TextureMaxClampSSgix = 33641,
        TextureMaxClampTSgix = 33642,
        TextureMaxClampRSgix = 33643,
        TextureLodBias = 34049,
        DepthTextureMode = 34891,
        TextureCompareMode = 34892,
        TextureCompareFunc = 34893,
        TextureSwizzleR = 36418,
        TextureSwizzleG = 36419,
        TextureSwizzleB = 36420,
        TextureSwizzleA = 36421,
        TextureSwizzleRgba = 36422
    }
    
    public enum TextureMinFilter
    {
        Nearest = 9728,
        Linear = 9729,
        NearestMipmapNearest = 9984,
        LinearMipmapNearest = 9985,
        NearestMipmapLinear = 9986,
        LinearMipmapLinear = 9987,
        Filter4Sgis = 33094,
        LinearClipmapLinearSgix = 33136,
        PixelTexGenQCeilingSgix = 33156,
        PixelTexGenQRoundSgix = 33157,
        PixelTexGenQFloorSgix = 33158,
        NearestClipmapNearestSgix = 33869,
        NearestClipmapLinearSgix = 33870,
        LinearClipmapNearestSgix = 33871
    }

    public enum TextureMagFilter
    {
        Nearest = 9728,
        Linear = 9729,
        LinearDetailSgis = 32919,
        LinearDetailAlphaSgis = 32920,
        LinearDetailColorSgis = 32921,
        LinearSharpenSgis = 32941,
        LinearSharpenAlphaSgis = 32942,
        LinearSharpenColorSgis = 32943,
        Filter4Sgis = 33094,
        PixelTexGenQCeilingSgix = 33156,
        PixelTexGenQRoundSgix = 33157,
        PixelTexGenQFloorSgix = 33158
    }
    
    public enum PrimitiveType : uint
    {
        Points = 0,
        Lines = 1,
        LineLoop = 2,
        LineStrip = 3,
        Triangles = 4,
        TriangleStrip = 5,
        TriangleFan = 6,
        Quads = 7,
        QuadsExt = 7,
        QuadStrip = 8,
        Polygon = 9,
        LinesAdjacency = 10,
        LinesAdjacencyArb = 10,
        LinesAdjacencyExt = 10,
        LineStripAdjacency = 11,
        LineStripAdjacencyArb = 11,
        LineStripAdjacencyExt = 11,
        TrianglesAdjacency = 12,
        TrianglesAdjacencyArb = 12,
        TrianglesAdjacencyExt = 12,
        TriangleStripAdjacency = 13,
        TriangleStripAdjacencyArb = 13,
        TriangleStripAdjacencyExt = 13,
        Patches = 14,
        PatchesExt = 14
    }
    
    public enum DrawBufferMode
    {
        None = 0,
        NoneOes = 0,
        FrontLeft = 1024,
        FrontRight = 1025,
        BackLeft = 1026,
        BackRight = 1027,
        Front = 1028,
        Back = 1029,
        Left = 1030,
        Right = 1031,
        FrontAndBack = 1032,
        Aux0 = 1033,
        Aux1 = 1034,
        Aux2 = 1035,
        Aux3 = 1036,
        ColorAttachment0 = 36064,
        ColorAttachment1 = 36065,
        ColorAttachment2 = 36066,
        ColorAttachment3 = 36067,
        ColorAttachment4 = 36068,
        ColorAttachment5 = 36069,
        ColorAttachment6 = 36070,
        ColorAttachment7 = 36071,
        ColorAttachment8 = 36072,
        ColorAttachment9 = 36073,
        ColorAttachment10 = 36074,
        ColorAttachment11 = 36075,
        ColorAttachment12 = 36076,
        ColorAttachment13 = 36077,
        ColorAttachment14 = 36078,
        ColorAttachment15 = 36079
    }

    [Flags]
    public enum ClearBufferMask
    {
        None = 0,
        DepthBufferBit = 256,
        AccumBufferBit = 512,
        StencilBufferBit = 1024,
        ColorBufferBit = 16384,
        CoverageBufferBitNv = 32768
    }

    public enum DrawElementsType
    {
        UnsignedByte = 5121,
        UnsignedShort = 5123,
        UnsignedInt = 5125
    }

    public enum TextureUnit
    {
        Texture0 = 33984,
        Texture1 = 33985,
        Texture2 = 33986,
        Texture3 = 33987,
        Texture4 = 33988,
        Texture5 = 33989,
        Texture6 = 33990,
        Texture7 = 33991,
        Texture8 = 33992,
        Texture9 = 33993,
        Texture10 = 33994,
        Texture11 = 33995,
        Texture12 = 33996,
        Texture13 = 33997,
        Texture14 = 33998,
        Texture15 = 33999,
        Texture16 = 34000,
        Texture17 = 34001,
        Texture18 = 34002,
        Texture19 = 34003,
        Texture20 = 34004,
        Texture21 = 34005,
        Texture22 = 34006,
        Texture23 = 34007,
        Texture24 = 34008,
        Texture25 = 34009,
        Texture26 = 34010,
        Texture27 = 34011,
        Texture28 = 34012,
        Texture29 = 34013,
        Texture30 = 34014,
        Texture31 = 34015
    }

    public enum FramebufferTarget
    {
        ReadFramebuffer = 36008,
        DrawFramebuffer = 36009,
        Framebuffer = 36160,
        FramebufferExt = 36160
    }

    public enum RenderbufferTarget
    {
        Renderbuffer = 36161
    }

    public enum GLFramebufferAttachment
    {
        FrontLeft = 1024,
        FrontRight = 1025,
        BackLeft = 1026,
        BackRight = 1027,
        Aux0 = 1033,
        Aux1 = 1034,
        Aux2 = 1035,
        Aux3 = 1036,
        Color = 6144,
        Depth = 6145,
        Stencil = 6146,
        DepthStencilAttachment = 33306,
        ColorAttachment0 = 36064,
        ColorAttachment0Ext = 36064,
        ColorAttachment1 = 36065,
        ColorAttachment1Ext = 36065,
        ColorAttachment2 = 36066,
        ColorAttachment2Ext = 36066,
        ColorAttachment3 = 36067,
        ColorAttachment3Ext = 36067,
        ColorAttachment4 = 36068,
        ColorAttachment4Ext = 36068,
        ColorAttachment5 = 36069,
        ColorAttachment5Ext = 36069,
        ColorAttachment6 = 36070,
        ColorAttachment6Ext = 36070,
        ColorAttachment7 = 36071,
        ColorAttachment7Ext = 36071,
        ColorAttachment8 = 36072,
        ColorAttachment8Ext = 36072,
        ColorAttachment9 = 36073,
        ColorAttachment9Ext = 36073,
        ColorAttachment10 = 36074,
        ColorAttachment10Ext = 36074,
        ColorAttachment11 = 36075,
        ColorAttachment11Ext = 36075,
        ColorAttachment12 = 36076,
        ColorAttachment12Ext = 36076,
        ColorAttachment13 = 36077,
        ColorAttachment13Ext = 36077,
        ColorAttachment14 = 36078,
        ColorAttachment14Ext = 36078,
        ColorAttachment15 = 36079,
        ColorAttachment15Ext = 36079,
        DepthAttachment = 36096,
        DepthAttachmentExt = 36096,
        StencilAttachment = 36128,
        StencilAttachmentExt = 36128
    }

    public enum DrawBuffersEnum
    {
        None = 0,
        FrontLeft = 1024,
        FrontRight = 1025,
        BackLeft = 1026,
        BackRight = 1027,
        Aux0 = 1033,
        Aux1 = 1034,
        Aux2 = 1035,
        Aux3 = 1036,
        ColorAttachment0 = 36064,
        ColorAttachment1 = 36065,
        ColorAttachment2 = 36066,
        ColorAttachment3 = 36067,
        ColorAttachment4 = 36068,
        ColorAttachment5 = 36069,
        ColorAttachment6 = 36070,
        ColorAttachment7 = 36071,
        ColorAttachment8 = 36072,
        ColorAttachment9 = 36073,
        ColorAttachment10 = 36074,
        ColorAttachment11 = 36075,
        ColorAttachment12 = 36076,
        ColorAttachment13 = 36077,
        ColorAttachment14 = 36078,
        ColorAttachment15 = 36079
    }

    public enum FramebufferErrorCode
    {
        FramebufferUndefined = 33305,
        FramebufferComplete = 36053,
        FramebufferCompleteExt = 36053,
        FramebufferIncompleteAttachment = 36054,
        FramebufferIncompleteAttachmentExt = 36054,
        FramebufferIncompleteMissingAttachment = 36055,
        FramebufferIncompleteMissingAttachmentExt = 36055,
        FramebufferIncompleteDimensionsExt = 36057,
        FramebufferIncompleteFormatsExt = 36058,
        FramebufferIncompleteDrawBuffer = 36059,
        FramebufferIncompleteDrawBufferExt = 36059,
        FramebufferIncompleteReadBuffer = 36060,
        FramebufferIncompleteReadBufferExt = 36060,
        FramebufferUnsupported = 36061,
        FramebufferUnsupportedExt = 36061,
        FramebufferIncompleteMultisample = 36182,
        FramebufferIncompleteLayerTargets = 36264,
        FramebufferIncompleteLayerCount = 36265
    }

    public enum BufferTarget
    {
        ArrayBuffer = 34962,
        ElementArrayBuffer = 34963,
        PixelPackBuffer = 35051,
        PixelUnpackBuffer = 35052,
        UniformBuffer = 35345,
        TextureBuffer = 35882,
        TransformFeedbackBuffer = 35982,
        CopyReadBuffer = 36662,
        CopyWriteBuffer = 36663,
        DrawIndirectBuffer = 36671,
        ShaderStorageBuffer = 37074,
        DispatchIndirectBuffer = 37102,
        QueryBuffer = 37266,
        AtomicCounterBuffer = 37568
    }

    public enum PixelStoreParameter
    {
        UnpackSwapBytes = 3312,
        UnpackLsbFirst = 3313,
        UnpackRowLength = 3314,
        UnpackRowLengthExt = 3314,
        UnpackSkipRows = 3315,
        UnpackSkipRowsExt = 3315,
        UnpackSkipPixels = 3316,
        UnpackSkipPixelsExt = 3316,
        UnpackAlignment = 3317,
        PackSwapBytes = 3328,
        PackLsbFirst = 3329,
        PackRowLength = 3330,
        PackSkipRows = 3331,
        PackSkipPixels = 3332,
        PackAlignment = 3333,
        PackSkipImages = 32875,
        PackSkipImagesExt = 32875,
        PackImageHeight = 32876,
        PackImageHeightExt = 32876,
        UnpackSkipImages = 32877,
        UnpackSkipImagesExt = 32877,
        UnpackImageHeight = 32878,
        UnpackImageHeightExt = 32878,
        PackSkipVolumesSgis = 33072,
        PackImageDepthSgis = 33073,
        UnpackSkipVolumesSgis = 33074,
        UnpackImageDepthSgis = 33075,
        PixelTileWidthSgix = 33088,
        PixelTileHeightSgix = 33089,
        PixelTileGridWidthSgix = 33090,
        PixelTileGridHeightSgix = 33091,
        PixelTileGridDepthSgix = 33092,
        PixelTileCacheSizeSgix = 33093,
        PackResampleSgix = 33836,
        UnpackResampleSgix = 33837,
        PackSubsampleRateSgix = 34208,
        UnpackSubsampleRateSgix = 34209,
        PackResampleOml = 35204,
        UnpackResampleOml = 35205,
        UnpackCompressedBlockWidth = 37159,
        UnpackCompressedBlockHeight = 37160,
        UnpackCompressedBlockDepth = 37161,
        UnpackCompressedBlockSize = 37162,
        PackCompressedBlockWidth = 37163,
        PackCompressedBlockHeight = 37164,
        PackCompressedBlockDepth = 37165,
        PackCompressedBlockSize = 37166
    }

    public enum ShaderType
    {
        FragmentShader = 35632,
        VertexShader = 35633,
        GeometryShader = 36313,
        GeometryShaderExt = 36313,
        TessEvaluationShader = 36487,
        TessControlShader = 36488,
        ComputeShader = 37305
    }

    public enum ShaderParameter
    {
        ShaderType = 35663,
        DeleteStatus = 35712,
        CompileStatus = 35713,
        InfoLogLength = 35716,
        ShaderSourceLength = 35720
    }

    public enum SamplerParameterName
    {
        TextureBorderColor = 4100,
        TextureMagFilter = 10240,
        TextureMinFilter = 10241,
        TextureWrapS = 10242,
        TextureWrapT = 10243,
        TextureWrapR = 32882,
        TextureMinLod = 33082,
        TextureMaxLod = 33083,
        TextureMaxAnisotropyExt = 34046,
        TextureLodBias = 34049,
        TextureCompareMode = 34892,
        TextureCompareFunc = 34893
    }

    public enum TextureWrapMode
    {
        Clamp = 10496,
        Repeat = 10497,
        ClampToBorder = 33069,
        ClampToBorderArb = 33069,
        ClampToBorderNv = 33069,
        ClampToBorderSgis = 33069,
        ClampToEdge = 33071,
        ClampToEdgeSgis = 33071,
        MirroredRepeat = 33648
    }

    public enum TextureCompareMode
    {
        None = 0,
        CompareRefToTexture = 34894,
        CompareRToTexture = 34894
    }

    public enum DepthFunction
    {
        Never = 512,
        Less = 513,
        Equal = 514,
        Lequal = 515,
        Greater = 516,
        Notequal = 517,
        Gequal = 518,
        Always = 519
    }

    public enum BlendEquationMode
    {
        FuncAdd = 32774,
        Min = 32775,
        Max = 32776,
        FuncSubtract = 32778,
        FuncReverseSubtract = 32779
    }

    public enum CullFaceMode
    {
        Front = 1028,
        Back = 1029,
        FrontAndBack = 1032
    }

    public enum MaterialFace
    {
        Front = 1028,
        Back = 1029,
        FrontAndBack = 1032
    }

    public enum PolygonMode
    {
        Point = 6912,
        Line = 6913,
        Fill = 6914
    }

    public enum GetProgramParameterName
    {
        ProgramBinaryRetrievableHint = 33367,
        ProgramSeparable = 33368,
        GeometryShaderInvocations = 34943,
        GeometryVerticesOut = 35094,
        GeometryInputType = 35095,
        GeometryOutputType = 35096,
        ActiveUniformBlockMaxNameLength = 35381,
        ActiveUniformBlocks = 35382,
        DeleteStatus = 35712,
        LinkStatus = 35714,
        ValidateStatus = 35715,
        InfoLogLength = 35716,
        AttachedShaders = 35717,
        ActiveUniforms = 35718,
        ActiveUniformMaxLength = 35719,
        ActiveAttributes = 35721,
        ActiveAttributeMaxLength = 35722,
        TransformFeedbackVaryingMaxLength = 35958,
        TransformFeedbackBufferMode = 35967,
        TransformFeedbackVaryings = 35971,
        TessControlOutputVertices = 36469,
        TessGenMode = 36470,
        TessGenSpacing = 36471,
        TessGenVertexOrder = 36472,
        TessGenPointMode = 36473,
        MaxComputeWorkGroupSize = 37311,
        ActiveAtomicCounterBuffers = 37593
    }

    public enum BufferRangeTarget
    {
        UniformBuffer = 35345,
        TransformFeedbackBuffer = 35982,
        ShaderStorageBuffer = 37074,
        AtomicCounterBuffer = 37568
    }

    public enum DebugSource
    {
        DebugSourceApi = 33350,
        DebugSourceWindowSystem = 33351,
        DebugSourceShaderCompiler = 33352,
        DebugSourceThirdParty = 33353,
        DebugSourceApplication = 33354,
        DebugSourceOther = 33355
    }

    public enum DebugType
    {
        DebugTypeError = 33356,
        DebugTypeDeprecatedBehavior = 33357,
        DebugTypeUndefinedBehavior = 33358,
        DebugTypePortability = 33359,
        DebugTypePerformance = 33360,
        DebugTypeOther = 33361,
        DebugTypeMarker = 33384,
        DebugTypePushGroup = 33385,
        DebugTypePopGroup = 33386
    }

    public enum DebugSeverity
    {
        DebugSeverityNotification = 33387,
        DebugSeverityHigh = 37190,
        DebugSeverityMedium = 37191,
        DebugSeverityLow = 37192
    }

    public enum BufferUsageHint
    {
        StreamDraw = 35040,
        StreamRead = 35041,
        StreamCopy = 35042,
        StaticDraw = 35044,
        StaticRead = 35045,
        StaticCopy = 35046,
        DynamicDraw = 35048,
        DynamicRead = 35049,
        DynamicCopy = 35050
    }

    public enum VertexAttribPointerType
    {
        Byte = 5120,
        UnsignedByte = 5121,
        Short = 5122,
        UnsignedShort = 5123,
        Int = 5124,
        UnsignedInt = 5125,
        Float = 5126,
        Double = 5130,
        HalfFloat = 5131,
        Fixed = 5132,
        UnsignedInt2101010Rev = 33640,
        Int2101010Rev = 36255
    }

    public enum FrontFaceDirection
    {
        Cw = 2304,
        Ccw = 2305
    }

    public enum GetPName
    {
        CurrentColor = 2816,
        CurrentIndex = 2817,
        CurrentNormal = 2818,
        CurrentTextureCoords = 2819,
        CurrentRasterColor = 2820,
        CurrentRasterIndex = 2821,
        CurrentRasterTextureCoords = 2822,
        CurrentRasterPosition = 2823,
        CurrentRasterPositionValid = 2824,
        CurrentRasterDistance = 2825,
        PointSmooth = 2832,
        PointSize = 2833,
        PointSizeRange = 2834,
        SmoothPointSizeRange = 2834,
        PointSizeGranularity = 2835,
        SmoothPointSizeGranularity = 2835,
        LineSmooth = 2848,
        LineWidth = 2849,
        LineWidthRange = 2850,
        SmoothLineWidthRange = 2850,
        LineWidthGranularity = 2851,
        SmoothLineWidthGranularity = 2851,
        LineStipple = 2852,
        LineStipplePattern = 2853,
        LineStippleRepeat = 2854,
        ListMode = 2864,
        MaxListNesting = 2865,
        ListBase = 2866,
        ListIndex = 2867,
        PolygonMode = 2880,
        PolygonSmooth = 2881,
        PolygonStipple = 2882,
        EdgeFlag = 2883,
        CullFace = 2884,
        CullFaceMode = 2885,
        FrontFace = 2886,
        Lighting = 2896,
        LightModelLocalViewer = 2897,
        LightModelTwoSide = 2898,
        LightModelAmbient = 2899,
        ShadeModel = 2900,
        ColorMaterialFace = 2901,
        ColorMaterialParameter = 2902,
        ColorMaterial = 2903,
        Fog = 2912,
        FogIndex = 2913,
        FogDensity = 2914,
        FogStart = 2915,
        FogEnd = 2916,
        FogMode = 2917,
        FogColor = 2918,
        DepthRange = 2928,
        DepthTest = 2929,
        DepthWritemask = 2930,
        DepthClearValue = 2931,
        DepthFunc = 2932,
        AccumClearValue = 2944,
        StencilTest = 2960,
        StencilClearValue = 2961,
        StencilFunc = 2962,
        StencilValueMask = 2963,
        StencilFail = 2964,
        StencilPassDepthFail = 2965,
        StencilPassDepthPass = 2966,
        StencilRef = 2967,
        StencilWritemask = 2968,
        MatrixMode = 2976,
        Normalize = 2977,
        Viewport = 2978,
        Modelview0StackDepthExt = 2979,
        ModelviewStackDepth = 2979,
        ProjectionStackDepth = 2980,
        TextureStackDepth = 2981,
        Modelview0MatrixExt = 2982,
        ModelviewMatrix = 2982,
        ProjectionMatrix = 2983,
        TextureMatrix = 2984,
        AttribStackDepth = 2992,
        ClientAttribStackDepth = 2993,
        AlphaTest = 3008,
        AlphaTestQcom = 3008,
        AlphaTestFunc = 3009,
        AlphaTestFuncQcom = 3009,
        AlphaTestRef = 3010,
        AlphaTestRefQcom = 3010,
        Dither = 3024,
        BlendDst = 3040,
        BlendSrc = 3041,
        Blend = 3042,
        LogicOpMode = 3056,
        IndexLogicOp = 3057,
        LogicOp = 3057,
        ColorLogicOp = 3058,
        AuxBuffers = 3072,
        DrawBuffer = 3073,
        DrawBufferExt = 3073,
        ReadBuffer = 3074,
        ReadBufferExt = 3074,
        ReadBufferNv = 3074,
        ScissorBox = 3088,
        ScissorTest = 3089,
        IndexClearValue = 3104,
        IndexWritemask = 3105,
        ColorClearValue = 3106,
        ColorWritemask = 3107,
        IndexMode = 3120,
        RgbaMode = 3121,
        Doublebuffer = 3122,
        Stereo = 3123,
        RenderMode = 3136,
        PerspectiveCorrectionHint = 3152,
        PointSmoothHint = 3153,
        LineSmoothHint = 3154,
        PolygonSmoothHint = 3155,
        FogHint = 3156,
        TextureGenS = 3168,
        TextureGenT = 3169,
        TextureGenR = 3170,
        TextureGenQ = 3171,
        PixelMapIToISize = 3248,
        PixelMapSToSSize = 3249,
        PixelMapIToRSize = 3250,
        PixelMapIToGSize = 3251,
        PixelMapIToBSize = 3252,
        PixelMapIToASize = 3253,
        PixelMapRToRSize = 3254,
        PixelMapGToGSize = 3255,
        PixelMapBToBSize = 3256,
        PixelMapAToASize = 3257,
        UnpackSwapBytes = 3312,
        UnpackLsbFirst = 3313,
        UnpackRowLength = 3314,
        UnpackSkipRows = 3315,
        UnpackSkipPixels = 3316,
        UnpackAlignment = 3317,
        PackSwapBytes = 3328,
        PackLsbFirst = 3329,
        PackRowLength = 3330,
        PackSkipRows = 3331,
        PackSkipPixels = 3332,
        PackAlignment = 3333,
        MapColor = 3344,
        MapStencil = 3345,
        IndexShift = 3346,
        IndexOffset = 3347,
        RedScale = 3348,
        RedBias = 3349,
        ZoomX = 3350,
        ZoomY = 3351,
        GreenScale = 3352,
        GreenBias = 3353,
        BlueScale = 3354,
        BlueBias = 3355,
        AlphaScale = 3356,
        AlphaBias = 3357,
        DepthScale = 3358,
        DepthBias = 3359,
        MaxEvalOrder = 3376,
        MaxLights = 3377,
        MaxClipDistances = 3378,
        MaxClipPlanes = 3378,
        MaxTextureSize = 3379,
        MaxPixelMapTable = 3380,
        MaxAttribStackDepth = 3381,
        MaxModelviewStackDepth = 3382,
        MaxNameStackDepth = 3383,
        MaxProjectionStackDepth = 3384,
        MaxTextureStackDepth = 3385,
        MaxViewportDims = 3386,
        MaxClientAttribStackDepth = 3387,
        SubpixelBits = 3408,
        IndexBits = 3409,
        RedBits = 3410,
        GreenBits = 3411,
        BlueBits = 3412,
        AlphaBits = 3413,
        DepthBits = 3414,
        StencilBits = 3415,
        AccumRedBits = 3416,
        AccumGreenBits = 3417,
        AccumBlueBits = 3418,
        AccumAlphaBits = 3419,
        NameStackDepth = 3440,
        AutoNormal = 3456,
        Map1Color4 = 3472,
        Map1Index = 3473,
        Map1Normal = 3474,
        Map1TextureCoord1 = 3475,
        Map1TextureCoord2 = 3476,
        Map1TextureCoord3 = 3477,
        Map1TextureCoord4 = 3478,
        Map1Vertex3 = 3479,
        Map1Vertex4 = 3480,
        Map2Color4 = 3504,
        Map2Index = 3505,
        Map2Normal = 3506,
        Map2TextureCoord1 = 3507,
        Map2TextureCoord2 = 3508,
        Map2TextureCoord3 = 3509,
        Map2TextureCoord4 = 3510,
        Map2Vertex3 = 3511,
        Map2Vertex4 = 3512,
        Map1GridDomain = 3536,
        Map1GridSegments = 3537,
        Map2GridDomain = 3538,
        Map2GridSegments = 3539,
        Texture1D = 3552,
        Texture2D = 3553,
        FeedbackBufferSize = 3569,
        FeedbackBufferType = 3570,
        SelectionBufferSize = 3572,
        PolygonOffsetUnits = 10752,
        PolygonOffsetPoint = 10753,
        PolygonOffsetLine = 10754,
        ClipPlane0 = 12288,
        ClipPlane1 = 12289,
        ClipPlane2 = 12290,
        ClipPlane3 = 12291,
        ClipPlane4 = 12292,
        ClipPlane5 = 12293,
        Light0 = 16384,
        Light1 = 16385,
        Light2 = 16386,
        Light3 = 16387,
        Light4 = 16388,
        Light5 = 16389,
        Light6 = 16390,
        Light7 = 16391,
        BlendColorExt = 32773,
        BlendEquationExt = 32777,
        BlendEquationRgb = 32777,
        PackCmykHintExt = 32782,
        UnpackCmykHintExt = 32783,
        Convolution1DExt = 32784,
        Convolution2DExt = 32785,
        Separable2DExt = 32786,
        PostConvolutionRedScaleExt = 32796,
        PostConvolutionGreenScaleExt = 32797,
        PostConvolutionBlueScaleExt = 32798,
        PostConvolutionAlphaScaleExt = 32799,
        PostConvolutionRedBiasExt = 32800,
        PostConvolutionGreenBiasExt = 32801,
        PostConvolutionBlueBiasExt = 32802,
        PostConvolutionAlphaBiasExt = 32803,
        HistogramExt = 32804,
        MinmaxExt = 32814,
        PolygonOffsetFill = 32823,
        PolygonOffsetFactor = 32824,
        PolygonOffsetBiasExt = 32825,
        RescaleNormalExt = 32826,
        TextureBinding1D = 32872,
        TextureBinding2D = 32873,
        Texture3DBindingExt = 32874,
        TextureBinding3D = 32874,
        PackSkipImagesExt = 32875,
        PackImageHeightExt = 32876,
        UnpackSkipImagesExt = 32877,
        UnpackImageHeightExt = 32878,
        Texture3DExt = 32879,
        Max3DTextureSize = 32883,
        Max3DTextureSizeExt = 32883,
        VertexArray = 32884,
        NormalArray = 32885,
        ColorArray = 32886,
        IndexArray = 32887,
        TextureCoordArray = 32888,
        EdgeFlagArray = 32889,
        VertexArraySize = 32890,
        VertexArrayType = 32891,
        VertexArrayStride = 32892,
        VertexArrayCountExt = 32893,
        NormalArrayType = 32894,
        NormalArrayStride = 32895,
        NormalArrayCountExt = 32896,
        ColorArraySize = 32897,
        ColorArrayType = 32898,
        ColorArrayStride = 32899,
        ColorArrayCountExt = 32900,
        IndexArrayType = 32901,
        IndexArrayStride = 32902,
        IndexArrayCountExt = 32903,
        TextureCoordArraySize = 32904,
        TextureCoordArrayType = 32905,
        TextureCoordArrayStride = 32906,
        TextureCoordArrayCountExt = 32907,
        EdgeFlagArrayStride = 32908,
        EdgeFlagArrayCountExt = 32909,
        InterlaceSgix = 32916,
        DetailTexture2DBindingSgis = 32918,
        Multisample = 32925,
        MultisampleSgis = 32925,
        SampleAlphaToCoverage = 32926,
        SampleAlphaToMaskSgis = 32926,
        SampleAlphaToOne = 32927,
        SampleAlphaToOneSgis = 32927,
        SampleCoverage = 32928,
        SampleMaskSgis = 32928,
        SampleBuffers = 32936,
        SampleBuffersSgis = 32936,
        Samples = 32937,
        SamplesSgis = 32937,
        SampleCoverageValue = 32938,
        SampleMaskValueSgis = 32938,
        SampleCoverageInvert = 32939,
        SampleMaskInvertSgis = 32939,
        SamplePatternSgis = 32940,
        ColorMatrixSgi = 32945,
        ColorMatrixStackDepthSgi = 32946,
        MaxColorMatrixStackDepthSgi = 32947,
        PostColorMatrixRedScaleSgi = 32948,
        PostColorMatrixGreenScaleSgi = 32949,
        PostColorMatrixBlueScaleSgi = 32950,
        PostColorMatrixAlphaScaleSgi = 32951,
        PostColorMatrixRedBiasSgi = 32952,
        PostColorMatrixGreenBiasSgi = 32953,
        PostColorMatrixBlueBiasSgi = 32954,
        PostColorMatrixAlphaBiasSgi = 32955,
        TextureColorTableSgi = 32956,
        BlendDstRgb = 32968,
        BlendSrcRgb = 32969,
        BlendDstAlpha = 32970,
        BlendSrcAlpha = 32971,
        ColorTableSgi = 32976,
        PostConvolutionColorTableSgi = 32977,
        PostColorMatrixColorTableSgi = 32978,
        MaxElementsVertices = 33000,
        MaxElementsIndices = 33001,
        PointSizeMin = 33062,
        PointSizeMinSgis = 33062,
        PointSizeMax = 33063,
        PointSizeMaxSgis = 33063,
        PointFadeThresholdSize = 33064,
        PointFadeThresholdSizeSgis = 33064,
        DistanceAttenuationSgis = 33065,
        PointDistanceAttenuation = 33065,
        FogFuncPointsSgis = 33067,
        MaxFogFuncPointsSgis = 33068,
        PackSkipVolumesSgis = 33072,
        PackImageDepthSgis = 33073,
        UnpackSkipVolumesSgis = 33074,
        UnpackImageDepthSgis = 33075,
        Texture4DSgis = 33076,
        Max4DTextureSizeSgis = 33080,
        PixelTexGenSgix = 33081,
        PixelTileBestAlignmentSgix = 33086,
        PixelTileCacheIncrementSgix = 33087,
        PixelTileWidthSgix = 33088,
        PixelTileHeightSgix = 33089,
        PixelTileGridWidthSgix = 33090,
        PixelTileGridHeightSgix = 33091,
        PixelTileGridDepthSgix = 33092,
        PixelTileCacheSizeSgix = 33093,
        SpriteSgix = 33096,
        SpriteModeSgix = 33097,
        SpriteAxisSgix = 33098,
        SpriteTranslationSgix = 33099,
        Texture4DBindingSgis = 33103,
        MaxClipmapDepthSgix = 33143,
        MaxClipmapVirtualDepthSgix = 33144,
        PostTextureFilterBiasRangeSgix = 33147,
        PostTextureFilterScaleRangeSgix = 33148,
        ReferencePlaneSgix = 33149,
        ReferencePlaneEquationSgix = 33150,
        IrInstrument1Sgix = 33151,
        InstrumentMeasurementsSgix = 33153,
        CalligraphicFragmentSgix = 33155,
        FramezoomSgix = 33163,
        FramezoomFactorSgix = 33164,
        MaxFramezoomFactorSgix = 33165,
        GenerateMipmapHint = 33170,
        GenerateMipmapHintSgis = 33170,
        DeformationsMaskSgix = 33174,
        FogOffsetSgix = 33176,
        FogOffsetValueSgix = 33177,
        LightModelColorControl = 33272,
        SharedTexturePaletteExt = 33275,
        MajorVersion = 33307,
        MinorVersion = 33308,
        NumExtensions = 33309,
        ContextFlags = 33310,
        ResetNotificationStrategy = 33366,
        ProgramPipelineBinding = 33370,
        MaxViewports = 33371,
        ViewportSubpixelBits = 33372,
        ViewportBoundsRange = 33373,
        LayerProvokingVertex = 33374,
        ViewportIndexProvokingVertex = 33375,
        MaxCullDistances = 33529,
        MaxCombinedClipAndCullDistances = 33530,
        ContextReleaseBehavior = 33531,
        ConvolutionHintSgix = 33558,
        AsyncMarkerSgix = 33577,
        PixelTexGenModeSgix = 33579,
        AsyncHistogramSgix = 33580,
        MaxAsyncHistogramSgix = 33581,
        PixelTextureSgis = 33619,
        AsyncTexImageSgix = 33628,
        AsyncDrawPixelsSgix = 33629,
        AsyncReadPixelsSgix = 33630,
        MaxAsyncTexImageSgix = 33631,
        MaxAsyncDrawPixelsSgix = 33632,
        MaxAsyncReadPixelsSgix = 33633,
        VertexPreclipSgix = 33774,
        VertexPreclipHintSgix = 33775,
        FragmentLightingSgix = 33792,
        FragmentColorMaterialSgix = 33793,
        FragmentColorMaterialFaceSgix = 33794,
        FragmentColorMaterialParameterSgix = 33795,
        MaxFragmentLightsSgix = 33796,
        MaxActiveLightsSgix = 33797,
        LightEnvModeSgix = 33799,
        FragmentLightModelLocalViewerSgix = 33800,
        FragmentLightModelTwoSideSgix = 33801,
        FragmentLightModelAmbientSgix = 33802,
        FragmentLightModelNormalInterpolationSgix = 33803,
        FragmentLight0Sgix = 33804,
        PackResampleSgix = 33836,
        UnpackResampleSgix = 33837,
        CurrentFogCoord = 33875,
        FogCoordArrayType = 33876,
        FogCoordArrayStride = 33877,
        ColorSum = 33880,
        CurrentSecondaryColor = 33881,
        SecondaryColorArraySize = 33882,
        SecondaryColorArrayType = 33883,
        SecondaryColorArrayStride = 33884,
        CurrentRasterSecondaryColor = 33887,
        AliasedPointSizeRange = 33901,
        AliasedLineWidthRange = 33902,
        ActiveTexture = 34016,
        ClientActiveTexture = 34017,
        MaxTextureUnits = 34018,
        TransposeModelviewMatrix = 34019,
        TransposeProjectionMatrix = 34020,
        TransposeTextureMatrix = 34021,
        TransposeColorMatrix = 34022,
        MaxRenderbufferSize = 34024,
        MaxRenderbufferSizeExt = 34024,
        TextureCompressionHint = 34031,
        TextureBindingRectangle = 34038,
        MaxRectangleTextureSize = 34040,
        MaxTextureLodBias = 34045,
        TextureCubeMap = 34067,
        TextureBindingCubeMap = 34068,
        MaxCubeMapTextureSize = 34076,
        PackSubsampleRateSgix = 34208,
        UnpackSubsampleRateSgix = 34209,
        VertexArrayBinding = 34229,
        ProgramPointSize = 34370,
        DepthClamp = 34383,
        NumCompressedTextureFormats = 34466,
        CompressedTextureFormats = 34467,
        NumProgramBinaryFormats = 34814,
        ProgramBinaryFormats = 34815,
        StencilBackFunc = 34816,
        StencilBackFail = 34817,
        StencilBackPassDepthFail = 34818,
        StencilBackPassDepthPass = 34819,
        RgbaFloatMode = 34848,
        MaxDrawBuffers = 34852,
        DrawBuffer0 = 34853,
        DrawBuffer1 = 34854,
        DrawBuffer2 = 34855,
        DrawBuffer3 = 34856,
        DrawBuffer4 = 34857,
        DrawBuffer5 = 34858,
        DrawBuffer6 = 34859,
        DrawBuffer7 = 34860,
        DrawBuffer8 = 34861,
        DrawBuffer9 = 34862,
        DrawBuffer10 = 34863,
        DrawBuffer11 = 34864,
        DrawBuffer12 = 34865,
        DrawBuffer13 = 34866,
        DrawBuffer14 = 34867,
        DrawBuffer15 = 34868,
        BlendEquationAlpha = 34877,
        TextureCubeMapSeamless = 34895,
        PointSprite = 34913,
        MaxVertexAttribs = 34921,
        MaxTessControlInputComponents = 34924,
        MaxTessEvaluationInputComponents = 34925,
        MaxTextureCoords = 34929,
        MaxTextureImageUnits = 34930,
        ArrayBufferBinding = 34964,
        ElementArrayBufferBinding = 34965,
        VertexArrayBufferBinding = 34966,
        NormalArrayBufferBinding = 34967,
        ColorArrayBufferBinding = 34968,
        IndexArrayBufferBinding = 34969,
        TextureCoordArrayBufferBinding = 34970,
        EdgeFlagArrayBufferBinding = 34971,
        SecondaryColorArrayBufferBinding = 34972,
        FogCoordArrayBufferBinding = 34973,
        WeightArrayBufferBinding = 34974,
        VertexAttribArrayBufferBinding = 34975,
        PixelPackBufferBinding = 35053,
        PixelUnpackBufferBinding = 35055,
        MaxDualSourceDrawBuffers = 35068,
        MaxArrayTextureLayers = 35071,
        MinProgramTexelOffset = 35076,
        MaxProgramTexelOffset = 35077,
        SamplerBinding = 35097,
        ClampVertexColor = 35098,
        ClampFragmentColor = 35099,
        ClampReadColor = 35100,
        MaxVertexUniformBlocks = 35371,
        MaxGeometryUniformBlocks = 35372,
        MaxFragmentUniformBlocks = 35373,
        MaxCombinedUniformBlocks = 35374,
        MaxUniformBufferBindings = 35375,
        MaxUniformBlockSize = 35376,
        MaxCombinedVertexUniformComponents = 35377,
        MaxCombinedGeometryUniformComponents = 35378,
        MaxCombinedFragmentUniformComponents = 35379,
        UniformBufferOffsetAlignment = 35380,
        MaxFragmentUniformComponents = 35657,
        MaxVertexUniformComponents = 35658,
        MaxVaryingComponents = 35659,
        MaxVaryingFloats = 35659,
        MaxVertexTextureImageUnits = 35660,
        MaxCombinedTextureImageUnits = 35661,
        FragmentShaderDerivativeHint = 35723,
        CurrentProgram = 35725,
        ImplementationColorReadType = 35738,
        ImplementationColorReadFormat = 35739,
        TextureBinding1DArray = 35868,
        TextureBinding2DArray = 35869,
        MaxGeometryTextureImageUnits = 35881,
        TextureBuffer = 35882,
        MaxTextureBufferSize = 35883,
        TextureBindingBuffer = 35884,
        TextureBufferDataStoreBinding = 35885,
        SampleShading = 35894,
        MinSampleShadingValue = 35895,
        MaxTransformFeedbackSeparateComponents = 35968,
        MaxTransformFeedbackInterleavedComponents = 35978,
        MaxTransformFeedbackSeparateAttribs = 35979,
        StencilBackRef = 36003,
        StencilBackValueMask = 36004,
        StencilBackWritemask = 36005,
        DrawFramebufferBinding = 36006,
        FramebufferBinding = 36006,
        FramebufferBindingExt = 36006,
        RenderbufferBinding = 36007,
        RenderbufferBindingExt = 36007,
        ReadFramebufferBinding = 36010,
        MaxColorAttachments = 36063,
        MaxColorAttachmentsExt = 36063,
        MaxSamples = 36183,
        FramebufferSrgb = 36281,
        MaxGeometryVaryingComponents = 36317,
        MaxVertexVaryingComponents = 36318,
        MaxGeometryUniformComponents = 36319,
        MaxGeometryOutputVertices = 36320,
        MaxGeometryTotalOutputComponents = 36321,
        MaxSubroutines = 36327,
        MaxSubroutineUniformLocations = 36328,
        ShaderBinaryFormats = 36344,
        NumShaderBinaryFormats = 36345,
        ShaderCompiler = 36346,
        MaxVertexUniformVectors = 36347,
        MaxVaryingVectors = 36348,
        MaxFragmentUniformVectors = 36349,
        MaxCombinedTessControlUniformComponents = 36382,
        MaxCombinedTessEvaluationUniformComponents = 36383,
        TransformFeedbackBufferPaused = 36387,
        TransformFeedbackBufferActive = 36388,
        TransformFeedbackBinding = 36389,
        Timestamp = 36392,
        QuadsFollowProvokingVertexConvention = 36428,
        ProvokingVertex = 36431,
        SampleMask = 36433,
        MaxSampleMaskWords = 36441,
        MaxGeometryShaderInvocations = 36442,
        MinFragmentInterpolationOffset = 36443,
        MaxFragmentInterpolationOffset = 36444,
        FragmentInterpolationOffsetBits = 36445,
        MinProgramTextureGatherOffset = 36446,
        MaxProgramTextureGatherOffset = 36447,
        MaxTransformFeedbackBuffers = 36464,
        MaxVertexStreams = 36465,
        PatchVertices = 36466,
        PatchDefaultInnerLevel = 36467,
        PatchDefaultOuterLevel = 36468,
        MaxPatchVertices = 36477,
        MaxTessGenLevel = 36478,
        MaxTessControlUniformComponents = 36479,
        MaxTessEvaluationUniformComponents = 36480,
        MaxTessControlTextureImageUnits = 36481,
        MaxTessEvaluationTextureImageUnits = 36482,
        MaxTessControlOutputComponents = 36483,
        MaxTessPatchComponents = 36484,
        MaxTessControlTotalOutputComponents = 36485,
        MaxTessEvaluationOutputComponents = 36486,
        MaxTessControlUniformBlocks = 36489,
        MaxTessEvaluationUniformBlocks = 36490,
        DrawIndirectBufferBinding = 36675,
        MaxVertexImageUniforms = 37066,
        MaxTessControlImageUniforms = 37067,
        MaxTessEvaluationImageUniforms = 37068,
        MaxGeometryImageUniforms = 37069,
        MaxFragmentImageUniforms = 37070,
        MaxCombinedImageUniforms = 37071,
        ContextRobustAccess = 37107,
        TextureBinding2DMultisample = 37124,
        TextureBinding2DMultisampleArray = 37125,
        MaxColorTextureSamples = 37134,
        MaxDepthTextureSamples = 37135,
        MaxIntegerSamples = 37136,
        MaxVertexOutputComponents = 37154,
        MaxGeometryInputComponents = 37155,
        MaxGeometryOutputComponents = 37156,
        MaxFragmentInputComponents = 37157,
        MaxComputeImageUniforms = 37309,
        ClipOrigin = 37724,
        ClipDepthMode = 37725
    }

    public enum StringName
    {
        Vendor = 0x1F00,
        Renderer = 0x1F01,
        Version = 0x1F02,
    }

    public enum StringNameIndexed
    {
        Extensions = 7939,
        ShadingLanguageVersion = 35724
    }

    public enum ObjectLabelIdentifier
    {
        Texture = 5890,
        VertexArray = 32884,
        Buffer = 33504,
        Shader = 33505,
        Program = 33506,
        Query = 33507,
        ProgramPipeline = 33508,
        Sampler = 33510,
        Framebuffer = 36160,
        Renderbuffer = 36161,
        TransformFeedback = 36386
    }

    public enum BlitFramebufferFilter
    {
        Nearest = 9728,
        Linear = 9729
    }

    public enum ErrorCode : int
    {
        NoError = ((int)0),
        InvalidEnum = ((int)0x0500),
        InvalidValue = ((int)0x0501),
        InvalidOperation = ((int)0x0502),
        StackOverflow = ((int)0x0503),
        StackUnderflow = ((int)0x0504),
        OutOfMemory = ((int)0x0505),
        InvalidFramebufferOperation = ((int)0x0506),
        InvalidFramebufferOperationExt = ((int)0x0506),
        InvalidFramebufferOperationOes = ((int)0x0506),
        ContextLost = ((int)0x0507),
        TableTooLarge = ((int)0x8031),
        TableTooLargeExt = ((int)0x8031),
        TextureTooLargeExt = ((int)0x8065),
    }

    public enum ProgramInterface : int
    {
        TransformFeedbackBuffer = ((int)0x8C8E),
        AtomicCounterBuffer = ((int)0x92C0),
        Uniform = ((int)0x92E1),
        UniformBlock = ((int)0x92E2),
        ProgramInput = ((int)0x92E3),
        ProgramOutput = ((int)0x92E4),
        BufferVariable = ((int)0x92E5),
        ShaderStorageBlock = ((int)0x92E6),
        VertexSubroutine = ((int)0x92E8),
        TessControlSubroutine = ((int)0x92E9),
        TessEvaluationSubroutine = ((int)0x92EA),
        GeometrySubroutine = ((int)0x92EB),
        FragmentSubroutine = ((int)0x92EC),
        ComputeSubroutine = ((int)0x92ED),
        VertexSubroutineUniform = ((int)0x92EE),
        TessControlSubroutineUniform = ((int)0x92EF),
        TessEvaluationSubroutineUniform = ((int)0x92F0),
        GeometrySubroutineUniform = ((int)0x92F1),
        FragmentSubroutineUniform = ((int)0x92F2),
        ComputeSubroutineUniform = ((int)0x92F3),
        TransformFeedbackVarying = ((int)0x92F4),
    }

    public enum TextureAccess : int
    {
        ReadOnly = ((int)0x88B8),
        WriteOnly = ((int)0x88B9),
        ReadWrite = ((int)0x88BA),
    }

    public enum SizedInternalFormat : int
    {
        Rgba8 = ((int)0x8058),
        Rgba16 = ((int)0x805B),
        R8 = ((int)0x8229),
        R16 = ((int)0x822A),
        Rg8 = ((int)0x822B),
        Rg16 = ((int)0x822C),
        R16f = ((int)0x822D),
        R32f = ((int)0x822E),
        Rg16f = ((int)0x822F),
        Rg32f = ((int)0x8230),
        R8i = ((int)0x8231),
        R8ui = ((int)0x8232),
        R16i = ((int)0x8233),
        R16ui = ((int)0x8234),
        R32i = ((int)0x8235),
        R32ui = ((int)0x8236),
        Rg8i = ((int)0x8237),
        Rg8ui = ((int)0x8238),
        Rg16i = ((int)0x8239),
        Rg16ui = ((int)0x823A),
        Rg32i = ((int)0x823B),
        Rg32ui = ((int)0x823C),
        Rgba32f = ((int)0x8814),
        Rgba16f = ((int)0x881A),
        Rgba32ui = ((int)0x8D70),
        Rgba16ui = ((int)0x8D76),
        Rgba8ui = ((int)0x8D7C),
        Rgba32i = ((int)0x8D82),
        Rgba16i = ((int)0x8D88),
        Rgba8i = ((int)0x8D8E),
    }

    public enum MemoryBarrierFlags : int
    {
        VertexAttribArrayBarrierBit = ((int)0x00000001),
        ElementArrayBarrierBit = ((int)0x00000002),
        UniformBarrierBit = ((int)0x00000004),
        TextureFetchBarrierBit = ((int)0x00000008),
        ShaderImageAccessBarrierBit = ((int)0x00000020),
        CommandBarrierBit = ((int)0x00000040),
        PixelBufferBarrierBit = ((int)0x00000080),
        TextureUpdateBarrierBit = ((int)0x00000100),
        BufferUpdateBarrierBit = ((int)0x00000200),
        FramebufferBarrierBit = ((int)0x00000400),
        TransformFeedbackBarrierBit = ((int)0x00000800),
        AtomicCounterBarrierBit = ((int)0x00001000),
        ShaderStorageBarrierBit = ((int)0x00002000),
        ClientMappedBufferBarrierBit = ((int)0x00004000),
        QueryBufferBarrierBit = ((int)0x00008000),
        AllBarrierBits = unchecked((int)0xFFFFFFFF),
    }


    public enum BufferAccess : int
    {
        ReadOnly = ((int)0x88B8),
        WriteOnly = ((int)0x88B9),
        ReadWrite = ((int)0x88BA),
    }

    [Flags]
    public enum BufferAccessMask : int
    {
        Read = ((int)0x0001),
        Write = ((int)0x0002),
        InvalidateRange = ((int)0x0004),
        InvalidateBuffer = ((int)0x0008),
        FlushExplicit = ((int)0x0010),
        Unsynchronized = ((int)0x0020),
        Persistent = ((int)0x0040),
        Coherent = ((int)0x0080),
    }

    public enum StencilFunction : int
    {
        Never = ((int)0x0200),
        Less = ((int)0x0201),
        Equal = ((int)0x0202),
        Lequal = ((int)0x0203),
        Greater = ((int)0x0204),
        Notequal = ((int)0x0205),
        Gequal = ((int)0x0206),
        Always = ((int)0x0207),
    }

    public enum StencilOp : int
    {
        Zero = ((int)0),
        Invert = ((int)0x150A),
        Keep = ((int)0x1E00),
        Replace = ((int)0x1E01),
        Incr = ((int)0x1E02),
        Decr = ((int)0x1E03),
        IncrWrap = ((int)0x8507),
        DecrWrap = ((int)0x8508),
    }

    public enum ActiveUniformBlockParameter : int
    {
        UniformBlockReferencedByTessControlShader = ((int)0x84F0),
        UniformBlockReferencedByTessEvaluationShader = ((int)0x84F1),
        UniformBlockBinding = ((int)0x8A3F),
        UniformBlockDataSize = ((int)0x8A40),
        UniformBlockNameLength = ((int)0x8A41),
        UniformBlockActiveUniforms = ((int)0x8A42),
        UniformBlockActiveUniformIndices = ((int)0x8A43),
        UniformBlockReferencedByVertexShader = ((int)0x8A44),
        UniformBlockReferencedByGeometryShader = ((int)0x8A45),
        UniformBlockReferencedByFragmentShader = ((int)0x8A46),
        UniformBlockReferencedByComputeShader = ((int)0x90EC),
    }

    public enum GetTextureParameter : int
    {
        TextureWidth = ((int)0x1000),
        TextureHeight = ((int)0x1001),
        TextureComponents = ((int)0x1003),
        TextureInternalFormat = ((int)0x1003),
        TextureBorderColor = ((int)0x1004),
        TextureBorderColorNv = ((int)0x1004),
        TextureBorder = ((int)0x1005),
        TextureTarget = ((int)0x1006),
        TextureMagFilter = ((int)0x2800),
        TextureMinFilter = ((int)0x2801),
        TextureWrapS = ((int)0x2802),
        TextureWrapT = ((int)0x2803),
        TextureRedSize = ((int)0x805C),
        TextureGreenSize = ((int)0x805D),
        TextureBlueSize = ((int)0x805E),
        TextureAlphaSize = ((int)0x805F),
        TextureLuminanceSize = ((int)0x8060),
        TextureIntensitySize = ((int)0x8061),
        TexturePriority = ((int)0x8066),
        TextureResident = ((int)0x8067),
        TextureDepth = ((int)0x8071),
        TextureDepthExt = ((int)0x8071),
        TextureWrapR = ((int)0x8072),
        TextureWrapRExt = ((int)0x8072),
        DetailTextureLevelSgis = ((int)0x809A),
        DetailTextureModeSgis = ((int)0x809B),
        DetailTextureFuncPointsSgis = ((int)0x809C),
        SharpenTextureFuncPointsSgis = ((int)0x80B0),
        ShadowAmbientSgix = ((int)0x80BF),
        DualTextureSelectSgis = ((int)0x8124),
        QuadTextureSelectSgis = ((int)0x8125),
        Texture4DsizeSgis = ((int)0x8136),
        TextureWrapQSgis = ((int)0x8137),
        TextureMinLod = ((int)0x813A),
        TextureMinLodSgis = ((int)0x813A),
        TextureMaxLod = ((int)0x813B),
        TextureMaxLodSgis = ((int)0x813B),
        TextureBaseLevel = ((int)0x813C),
        TextureBaseLevelSgis = ((int)0x813C),
        TextureMaxLevel = ((int)0x813D),
        TextureMaxLevelSgis = ((int)0x813D),
        TextureFilter4SizeSgis = ((int)0x8147),
        TextureClipmapCenterSgix = ((int)0x8171),
        TextureClipmapFrameSgix = ((int)0x8172),
        TextureClipmapOffsetSgix = ((int)0x8173),
        TextureClipmapVirtualDepthSgix = ((int)0x8174),
        TextureClipmapLodOffsetSgix = ((int)0x8175),
        TextureClipmapDepthSgix = ((int)0x8176),
        PostTextureFilterBiasSgix = ((int)0x8179),
        PostTextureFilterScaleSgix = ((int)0x817A),
        TextureLodBiasSSgix = ((int)0x818E),
        TextureLodBiasTSgix = ((int)0x818F),
        TextureLodBiasRSgix = ((int)0x8190),
        GenerateMipmap = ((int)0x8191),
        GenerateMipmapSgis = ((int)0x8191),
        TextureCompareSgix = ((int)0x819A),
        TextureCompareOperatorSgix = ((int)0x819B),
        TextureLequalRSgix = ((int)0x819C),
        TextureGequalRSgix = ((int)0x819D),
        TextureViewMinLevel = ((int)0x82DB),
        TextureViewNumLevels = ((int)0x82DC),
        TextureViewMinLayer = ((int)0x82DD),
        TextureViewNumLayers = ((int)0x82DE),
        TextureImmutableLevels = ((int)0x82DF),
        TextureMaxClampSSgix = ((int)0x8369),
        TextureMaxClampTSgix = ((int)0x836A),
        TextureMaxClampRSgix = ((int)0x836B),
        TextureCompressedImageSize = ((int)0x86A0),
        TextureCompressed = ((int)0x86A1),
        TextureDepthSize = ((int)0x884A),
        DepthTextureMode = ((int)0x884B),
        TextureCompareMode = ((int)0x884C),
        TextureCompareFunc = ((int)0x884D),
        TextureStencilSize = ((int)0x88F1),
        TextureRedType = ((int)0x8C10),
        TextureGreenType = ((int)0x8C11),
        TextureBlueType = ((int)0x8C12),
        TextureAlphaType = ((int)0x8C13),
        TextureLuminanceType = ((int)0x8C14),
        TextureIntensityType = ((int)0x8C15),
        TextureDepthType = ((int)0x8C16),
        TextureSharedSize = ((int)0x8C3F),
        TextureSwizzleR = ((int)0x8E42),
        TextureSwizzleG = ((int)0x8E43),
        TextureSwizzleB = ((int)0x8E44),
        TextureSwizzleA = ((int)0x8E45),
        TextureSwizzleRgba = ((int)0x8E46),
        ImageFormatCompatibilityType = ((int)0x90C7),
        TextureSamples = ((int)0x9106),
        TextureFixedSampleLocations = ((int)0x9107),
        TextureImmutableFormat = ((int)0x912F),
    }

    public enum RenderbufferPname
    {
        RenderbufferWidth = 0x8D42,
        RenderbufferHeight = 0x8D43,
    }

    public enum ClipControlOrigin
    {
        LowerLeft = 0x8CA1,
        UpperLeft = 0x8CA2,
    }

    public enum ClipControlDepthRange
    {
        NegativeOneToOne = 0x935E,
        ZeroToOne = 0x935F,
    }
    
    internal static class GL
    {
        private const CallingConvention CallConv = CallingConvention.Winapi;

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glEnable_d(EnableCap cap);

        private static glEnable_d glEnable_f;
        public static void Enable(EnableCap cap) => glEnable_f(cap);


        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glDisable_t(EnableCap cap);
        private static glDisable_t p_glDisable;
        public static void Disable(EnableCap cap) => p_glDisable(cap);


        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glGenTextures_d(int n, out uint textures);

        private static glGenTextures_d glGenTextures_f;
        public static void GenTextures(int n, out uint textures) => glGenTextures_f(n, out textures);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glBindTexture_d(TextureTarget target, uint texture);

        private static glBindTexture_d glBindTexture_f;
        public static void BindTexture(TextureTarget target, uint texture) => glBindTexture_f(target, texture);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glTexImage2D_d(TextureTarget target, int level, PixelInternalFormat internalformat, int width, int height,
            int border, GLPixelFormat format, GLPixelType type, IntPtr pixels);

        private static glTexImage2D_d glTexImage2D_f;

        public static void TexImage2D(TextureTarget target, int level, PixelInternalFormat internalformat, int width, int height, int border,
            GLPixelFormat format, GLPixelType type, IntPtr pixels) => glTexImage2D_f(target, level, internalformat, width, height,
            border, format, type, pixels);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glBlendFunc_d(BlendingFactorSrc sfactor, BlendingFactorDest dfactor);

        private static glBlendFunc_d glBlendFunc_f;
        public static void BlendFunc(BlendingFactorSrc sfactor, BlendingFactorDest dfactor) => glBlendFunc_f(sfactor, dfactor);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glTexEnvf_d(TextureTarget target, uint pname, float param);

        private static glTexEnvf_d glTexEnvf_f;
        public static void TexEnvf(TextureTarget target, uint pname, float param) => glTexEnvf_f(target, pname, param);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glTexParameterf_d(TextureTarget target, TextureParameterName pname, float param);

        private static glTexParameterf_d glTexParameterf_f;

        public static void TexParameterf(TextureTarget target, TextureParameterName pname, float param) =>
            glTexParameterf_f(target, pname, param);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glTexParameteri_d(TextureTarget target, TextureParameterName pname, int param);

        private static glTexParameteri_d glTexParameteri_f;
        public static void TexParameteri(TextureTarget target, TextureParameterName pname, int param) => glTexParameteri_f(target, pname, param);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glViewport_d(int x, int y, int width, int height);

        private static glViewport_d glViewport_f;
        public static void Viewport(int x, int y, int width, int height) => glViewport_f(x, y, width, height);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glTexSubImage2D_d(TextureTarget target, int level, int xoffset, int yoffset, int width, int height,
            GLPixelFormat format, GLPixelType type, IntPtr pixels);

        private static glTexSubImage2D_d glTexSubImage2D_f;

        public static void TexSubImage2D(TextureTarget target, int level, int xoffset, int yoffset, int width, int height,
            GLPixelFormat format, GLPixelType type, IntPtr pixels) =>
            glTexSubImage2D_f(target, level, xoffset, yoffset, width, height, format, type, pixels);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glBegin_d(PrimitiveType mode);

        private static glBegin_d glBegin_f;
        public static void Begin(PrimitiveType mode) => glBegin_f(mode);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glTexCoord2i_d(int s, int t);

        private static glTexCoord2i_d glTexCoord2i_f;
        public static void TexCoord2i(int s, int t) => glTexCoord2i_f(s, t);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glTexCoord2f_d(float s, float t);

        private static glTexCoord2f_d glTexCoord2f_f;
        public static void TexCoord2f(float s, float t) => glTexCoord2f_f(s, t);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glVertex2i_d(int x, int y);

        private static glVertex2i_d glVertex2i_f;
        public static void Vertex2i(int x, int y) => glVertex2i_f(x, y);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glVertex2f_d(float x, float y);

        private static glVertex2f_d glVertex2f_f;
        public static void Vertex2f(float x, float y) => glVertex2f_f(x, y);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glEnd_d();

        private static glEnd_d glEnd_f;
        public static void End() => glEnd_f();

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glGenVertexArrays_t(uint n, out uint arrays);
        private static glGenVertexArrays_t p_glGenVertexArrays;
        public static void GenVertexArrays(uint n, out uint arrays) => p_glGenVertexArrays(n, out arrays);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate uint glGetError_t();
        private static glGetError_t p_glGetError;
        public static uint GetError() => p_glGetError();

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glBindVertexArray_t(uint array);
        private static glBindVertexArray_t p_glBindVertexArray;
        public static void BindVertexArray(uint array) => p_glBindVertexArray(array);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glClearColor_t(float red, float green, float blue, float alpha);
        private static glClearColor_t p_glClearColor;
        public static void ClearColor(float red, float green, float blue, float alpha)
            => p_glClearColor(red, green, blue, alpha);


        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glClear_t(ClearBufferMask mask);
        private static glClear_t p_glClear;
        public static void Clear(ClearBufferMask mask) => p_glClear(mask);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glDrawElements_t(PrimitiveType mode, uint count, DrawElementsType type, IntPtr indices);
        private static glDrawElements_t p_glDrawElements;
        public static void DrawElements(PrimitiveType mode, uint count, DrawElementsType type, IntPtr indices)
            => p_glDrawElements(mode, count, type, indices);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glDrawArrays_t(PrimitiveType mode, int first, uint count);
        private static glDrawArrays_t p_glDrawArrays;
        public static void DrawArrays(PrimitiveType mode, int first, uint count) => p_glDrawArrays(mode, first, count);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glGenBuffers_t(uint n, out uint buffers);
        private static glGenBuffers_t p_glGenBuffers;
        public static void GenBuffers(uint n, out uint buffers) => p_glGenBuffers(n, out buffers);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glDeleteBuffers_t(uint n, ref uint buffers);
        private static glDeleteBuffers_t p_glDeleteBuffers;
        public static void DeleteBuffers(uint n, ref uint buffers) => p_glDeleteBuffers(n, ref buffers);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glGenFramebuffers_t(uint n, out uint ids);
        private static glGenFramebuffers_t p_glGenFramebuffers;
        public static void GenFramebuffers(uint n, out uint ids) => p_glGenFramebuffers(n, out ids);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glActiveTexture_t(TextureUnit texture);
        private static glActiveTexture_t p_glActiveTexture;
        public static void ActiveTexture(TextureUnit texture) => p_glActiveTexture(texture);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glFramebufferTexture2D_t(
            FramebufferTarget target,
            GLFramebufferAttachment attachment,
            TextureTarget textarget,
            uint texture,
            int level);
        private static glFramebufferTexture2D_t p_glFramebufferTexture2D;
        public static void FramebufferTexture2D(
            FramebufferTarget target,
            GLFramebufferAttachment attachment,
            TextureTarget textarget,
            uint texture,
            int level) => p_glFramebufferTexture2D(target, attachment, textarget, texture, level);


        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glBindFramebuffer_t(FramebufferTarget target, uint framebuffer);
        private static glBindFramebuffer_t p_glBindFramebuffer;
        public static void BindFramebuffer(FramebufferTarget target, uint framebuffer)
            => p_glBindFramebuffer(target, framebuffer);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glDeleteFramebuffers_t(uint n, ref uint framebuffers);
        private static glDeleteFramebuffers_t p_glDeleteFramebuffers;
        public static void DeleteFramebuffers(uint n, ref uint framebuffers) => p_glDeleteFramebuffers(n, ref framebuffers);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glGenTextures_t(uint n, out uint textures);
        private static glGenTextures_t p_glGenTextures;
        public static void GenTextures(uint n, out uint textures) => p_glGenTextures(n, out textures);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glDeleteTextures_t(uint n, ref uint textures);
        private static glDeleteTextures_t p_glDeleteTextures;
        public static void DeleteTextures(uint n, ref uint textures) => p_glDeleteTextures(n, ref textures);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate FramebufferErrorCode glCheckFramebufferStatus_t(FramebufferTarget target);
        private static glCheckFramebufferStatus_t p_glCheckFramebufferStatus;
        public static FramebufferErrorCode CheckFramebufferStatus(FramebufferTarget target)
            => p_glCheckFramebufferStatus(target);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glBindBuffer_t(BufferTarget target, uint buffer);
        private static glBindBuffer_t p_glBindBuffer;
        public static void BindBuffer(BufferTarget target, uint buffer) => p_glBindBuffer(target, buffer);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glBufferSubData_t(BufferTarget target, IntPtr offset, UIntPtr size, IntPtr data);
        private static glBufferSubData_t p_glBufferSubData;
        public static void BufferSubData(BufferTarget target, IntPtr offset, UIntPtr size, IntPtr data)
            => p_glBufferSubData(target, offset, size, data);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glScissor_t(int x, int y, uint width, uint height);
        private static glScissor_t p_glScissor;
        public static void Scissor(int x, int y, uint width, uint height) => p_glScissor(x, y, width, height);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glPixelStorei_t(PixelStoreParameter pname, int param);
        private static glPixelStorei_t p_glPixelStorei;
        public static void PixelStorei(PixelStoreParameter pname, int param) => p_glPixelStorei(pname, param);

        /*[UnmanagedFunctionPointer(CallConv)]
        private delegate void glShaderSource_t(uint shader, uint count, byte** @string, int* length);
        private static glShaderSource_t p_glShaderSource;
        public static void glShaderSource(uint shader, uint count, byte** @string, int* length)
            => p_glShaderSource(shader, count, @string, length);*/

        [UnmanagedFunctionPointer(CallConv)]
        private delegate uint glCreateShader_t(ShaderType shaderType);
        private static glCreateShader_t p_glCreateShader;
        public static uint CreateShader(ShaderType shaderType) => p_glCreateShader(shaderType);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glCompileShader_t(uint shader);
        private static glCompileShader_t p_glCompileShader;
        public static void CompileShader(uint shader) => p_glCompileShader(shader);

        /*[UnmanagedFunctionPointer(CallConv)]
        private delegate void glGetShaderiv_t(uint shader, ShaderParameter pname, int* @params);
        private static glGetShaderiv_t p_glGetShaderiv;
        public static void glGetShaderiv(uint shader, ShaderParameter pname, int* @params)
            => p_glGetShaderiv(shader, pname, @params);*/

        /*[UnmanagedFunctionPointer(CallConv)]
        private delegate void glGetShaderInfoLog_t(uint shader, uint maxLength, uint* length, byte* infoLog);
        private static glGetShaderInfoLog_t p_glGetShaderInfoLog;
        public static void glGetShaderInfoLog(uint shader, uint maxLength, uint* length, byte* infoLog)
            => p_glGetShaderInfoLog(shader, maxLength, length, infoLog);*/

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glDeleteShader_t(uint shader);
        private static glDeleteShader_t p_glDeleteShader;
        public static void DeleteShader(uint shader) => p_glDeleteShader(shader);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glBlendColor_t(float red, float green, float blue, float alpha);
        private static glBlendColor_t p_glBlendColor;
        public static void BlendColor(float red, float green, float blue, float alpha)
            => p_glBlendColor(red, green, blue, alpha);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate uint glCreateProgram_t();
        private static glCreateProgram_t p_glCreateProgram;
        public static uint CreateProgram() => p_glCreateProgram();

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glAttachShader_t(uint program, uint shader);
        private static glAttachShader_t p_glAttachShader;
        public static void AttachShader(uint program, uint shader) => p_glAttachShader(program, shader);

        /*[UnmanagedFunctionPointer(CallConv)]
        private delegate void glBindAttribLocation_t(uint program, uint index, byte* name);
        private static glBindAttribLocation_t p_glBindAttribLocation;
        public static void BindAttribLocation(uint program, uint index, byte* name)
            => p_glBindAttribLocation(program, index, name);*/

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glLinkProgram_t(uint program);
        private static glLinkProgram_t p_glLinkProgram;
        public static void LinkProgram(uint program) => p_glLinkProgram(program);

        /*[UnmanagedFunctionPointer(CallConv)]
        private delegate void glGetProgramiv_t(uint program, GetProgramParameterName pname, int* @params);
        private static glGetProgramiv_t p_glGetProgramiv;
        public static void GetProgramiv(uint program, GetProgramParameterName pname, int* @params)
            => p_glGetProgramiv(program, pname, @params);*/

        /*[UnmanagedFunctionPointer(CallConv)]
        private delegate void glGetProgramInfoLog_t(uint program, uint maxLength, uint* length, byte* infoLog);
        private static glGetProgramInfoLog_t p_glGetProgramInfoLog;
        public static void glGetProgramInfoLog(uint program, uint maxLength, uint* length, byte* infoLog)
            => p_glGetProgramInfoLog(program, maxLength, length, infoLog);*/

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glDeleteProgram_t(uint program);
        private static glDeleteProgram_t p_glDeleteProgram;
        public static void DeleteProgram(uint program) => p_glDeleteProgram(program);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glUniform1i_t(int location, int v0);
        private static glUniform1i_t p_glUniform1i;
        public static void Uniform1i(int location, int v0) => p_glUniform1i(location, v0);

        /*[UnmanagedFunctionPointer(CallConv)]
        private delegate int glGetUniformLocation_t(uint program, byte* name);
        private static glGetUniformLocation_t p_glGetUniformLocation;
        public static int GetUniformLocation(uint program, byte* name) => p_glGetUniformLocation(program, name);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate int glGetAttribLocation_t(uint program, byte* name);
        private static glGetAttribLocation_t p_glGetAttribLocation;
        public static int GetAttribLocation(uint program, byte* name) => p_glGetAttribLocation(program, name);*/

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glUseProgram_t(uint program);
        private static glUseProgram_t p_glUseProgram;
        public static void UseProgram(uint program) => p_glUseProgram(program);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glBufferData_t(BufferTarget target, UIntPtr size, IntPtr data, BufferUsageHint usage);
        private static glBufferData_t p_glBufferData;
        public static void BufferData(BufferTarget target, UIntPtr size, IntPtr data, BufferUsageHint usage)
            => p_glBufferData(target, size, data, usage);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glEnableVertexAttribArray_t(uint index);
        private static glEnableVertexAttribArray_t p_glEnableVertexAttribArray;
        public static void EnableVertexAttribArray(uint index) => p_glEnableVertexAttribArray(index);

        [UnmanagedFunctionPointer(CallConv)]
        private delegate void glDisableVertexAttribArray_t(uint index);
        private static glDisableVertexAttribArray_t p_glDisableVertexAttribArray;
        public static void DisableVertexAttribArray(uint index) => p_glDisableVertexAttribArray(index);

        /*[UnmanagedFunctionPointer(CallConv)]
        private delegate void VertexAttribPointer_t(
            uint index,
            int size,
            VertexAttribPointerType type,
            GLboolean normalized,
            uint stride,
            IntPtr pointer);
        private static glVertexAttribPointer_t p_glVertexAttribPointer;
        public static void glVertexAttribPointer(
            uint index,
            int size,
            VertexAttribPointerType type,
            GLboolean normalized,
            uint stride,
            IntPtr pointer) => p_glVertexAttribPointer(index, size, type, normalized, stride, pointer);*/

        internal static void LoadFunctions()
        {
            LoadFunction("glEnable", out glEnable_f);
            LoadFunction("glDisable", out p_glDisable);
            LoadFunction("glBegin", out glBegin_f);   
            LoadFunction("glGenTextures", out glGenTextures_f);
            LoadFunction("glBindTexture", out glBindTexture_f);
            LoadFunction("glTexImage2D", out glTexImage2D_f);
            LoadFunction("glBlendFunc", out glBlendFunc_f);
            LoadFunction("glTexEnvf", out glTexEnvf_f);
            LoadFunction("glTexParameteri", out glTexParameteri_f);
            LoadFunction("glTexParameterf", out glTexParameterf_f);
            LoadFunction("glViewport", out glViewport_f);
            LoadFunction("glTexSubImage2D", out glTexSubImage2D_f);
            LoadFunction("glTexCoord2i", out glTexCoord2i_f);
            LoadFunction("glTexCoord2f", out glTexCoord2f_f);
            LoadFunction("glVertex2i", out glVertex2i_f);
            LoadFunction("glVertex2f", out glVertex2f_f);
            LoadFunction("glEnd", out glEnd_f);
            LoadFunction("glGenVertexArrays", out p_glGenVertexArrays);
            LoadFunction("glGetError", out p_glGetError);
            LoadFunction("glBindVertexArray", out p_glBindVertexArray);
            LoadFunction("glClearColor", out p_glClearColor);
            LoadFunction("glClear", out p_glClear);
            LoadFunction("glDrawElements", out p_glDrawElements);
            LoadFunction("glDrawArrays", out p_glDrawArrays);
            LoadFunction("glGenBuffers", out p_glGenBuffers);
            LoadFunction("glDeleteBuffers", out p_glDeleteBuffers);
            LoadFunction("glGenFramebuffers", out p_glGenFramebuffers);
            LoadFunction("glActiveTexture", out p_glActiveTexture);
            LoadFunction("glFramebufferTexture2D", out p_glFramebufferTexture2D);
            LoadFunction("glBindFramebuffer", out p_glBindFramebuffer);
            LoadFunction("glDeleteFramebuffers", out p_glDeleteFramebuffers);
            LoadFunction("glGenTextures", out p_glGenTextures);
            LoadFunction("glDeleteTextures", out p_glDeleteTextures);
            LoadFunction("glCheckFramebufferStatus", out p_glCheckFramebufferStatus);
            LoadFunction("glBindBuffer", out p_glBindBuffer);
            LoadFunction("glBufferSubData", out p_glBufferSubData);
            LoadFunction("glPixelStorei", out p_glPixelStorei);
            LoadFunction("glCreateShader", out p_glCreateShader);
            LoadFunction("glCompileShader", out p_glCompileShader);
            LoadFunction("glDeleteShader", out p_glDeleteShader);
            LoadFunction("glBlendColor", out p_glBlendColor);
            LoadFunction("glCreateProgram", out p_glCreateProgram);
            LoadFunction("glAttachShader", out p_glAttachShader);
            LoadFunction("glLinkProgram", out p_glLinkProgram);
            LoadFunction("glDeleteProgram", out p_glDeleteProgram);
            LoadFunction("glUniform1i", out p_glUniform1i);
            LoadFunction("glUseProgram", out p_glUseProgram);
            LoadFunction("glBufferData", out p_glBufferData);
            LoadFunction("glEnableVertexAttribArray", out p_glEnableVertexAttribArray);
            LoadFunction("glDisableVertexAttribArray", out p_glDisableVertexAttribArray);
            LoadFunction("glScissor", out p_glScissor);

        }

        private static void LoadFunction<T>(string name, out T field)
        {
            IntPtr func_ptr = SDL.GL.GetProcAddress(name);

            if (func_ptr != IntPtr.Zero)
            {
                field = Marshal.GetDelegateForFunctionPointer<T>(func_ptr);
            }
            else
            {
                field = default(T);
            }
            
            
        }
    }
}