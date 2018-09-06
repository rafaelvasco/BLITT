using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using NativeLibraryLoader;

namespace BLITTEngine.Foundation
{
    internal static unsafe class SDL
    {
        private static readonly NativeLibrary s_sdl2Lib = LoadSdl2();

        private static NativeLibrary LoadSdl2()
        {
            string[] names;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                names = new[] {"SDL2.dll"};
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                names = new[]
                {
                    "libSDL2-2.0.so",
                    "libSDL2-2.0.so.0",
                    "libSDL2-2.0.so.1",
                };
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                names = new[]
                {
                    "libsdl2.dylib"
                };
            }
            else
            {
                Debug.WriteLine("Unknown SDL platform. Attempting to load \"SDL2\"");
                names = new[] {"SDL2.dll"};
            }

            NativeLibrary lib = new NativeLibrary(names);

            SDL_AddEventWatch_f = lib.LoadFunction<SDL_AddEventWatch_d>(nameof(SDL_AddEventWatch));
            SDL_AllocFormat_f = lib.LoadFunction<SDL_AllocFormat_d>(nameof(SDL_AllocFormat));
            SDL_AllocPalette_f = lib.LoadFunction<SDL_AllocPalette_d>(nameof(SDL_AllocPalette));
            SDL_CalculateGammaRamp_f = lib.LoadFunction<SDL_CalculateGammaRamp_d>(nameof(SDL_CalculateGammaRamp));
            SDL_CaptureMouse_f = lib.LoadFunction<SDL_CaptureMouse_d>(nameof(SDL_CaptureMouse));
            SDL_ClearError_f = lib.LoadFunction<SDL_ClearError_d>(nameof(SDL_ClearError));
            SDL_ComposeCustomBlendMode_f =
                lib.LoadFunction<SDL_ComposeCustomBlendMode_d>(nameof(SDL_ComposeCustomBlendMode));
            SDL_ConvertPixels_f = lib.LoadFunction<SDL_ConvertPixels_d>(nameof(SDL_ConvertPixels));
            SDL_ConvertSurface_f = lib.LoadFunction<SDL_ConvertSurface_d>(nameof(SDL_ConvertSurface));
            SDL_ConvertSurfaceFormat_f = lib.LoadFunction<SDL_ConvertSurfaceFormat_d>(nameof(SDL_ConvertSurfaceFormat));
            SDL_CreateCursor_f = lib.LoadFunction<SDL_CreateCursor_d>(nameof(SDL_CreateCursor));
            SDL_CreateRenderer_f = lib.LoadFunction<SDL_CreateRenderer_d>(nameof(SDL_CreateRenderer));
            SDL_CreateRGBSurface_f = lib.LoadFunction<SDL_CreateRGBSurface_d>(nameof(SDL_CreateRGBSurface));
            SDL_CreateRGBSurfaceFrom_f = lib.LoadFunction<SDL_CreateRGBSurfaceFrom_d>(nameof(SDL_CreateRGBSurfaceFrom));
            SDL_CreateRGBSurfaceWithFormatFrom_f =
                lib.LoadFunction<SDL_CreateRGBSurfaceWithFormatFrom_d>(nameof(SDL_CreateRGBSurfaceWithFormatFrom));
            SDL_CreateRGBSurfaceWithFormat_f =
                lib.LoadFunction<SDL_CreateRGBSurfaceWithFormat_d>(nameof(SDL_CreateRGBSurfaceWithFormat));
            SDL_CreateSoftwareRenderer_f =
                lib.LoadFunction<SDL_CreateSoftwareRenderer_d>(nameof(SDL_CreateSoftwareRenderer));
            SDL_CreateSystemCursor_f = lib.LoadFunction<SDL_CreateSystemCursor_d>(nameof(SDL_CreateSystemCursor));
            SDL_CreateTexture_f = lib.LoadFunction<SDL_CreateTexture_d>(nameof(SDL_CreateTexture));
            SDL_CreateTextureFromSurface_f =
                lib.LoadFunction<SDL_CreateTextureFromSurface_d>(nameof(SDL_CreateTextureFromSurface));
            SDL_CreateWindow_f = lib.LoadFunction<SDL_CreateWindow_d>(nameof(SDL_CreateWindow));
            SDL_CreateWindowAndRenderer_f =
                lib.LoadFunction<SDL_CreateWindowAndRenderer_d>(nameof(SDL_CreateWindowAndRenderer));
            SDL_CreateWindowFrom_f = lib.LoadFunction<SDL_CreateWindowFrom_d>(nameof(SDL_CreateWindowFrom));
            SDL_DelEventWatch_f = lib.LoadFunction<SDL_DelEventWatch_d>(nameof(SDL_DelEventWatch));
            SDL_DestroyRenderer_f = lib.LoadFunction<SDL_DestroyRenderer_d>(nameof(SDL_DestroyRenderer));
            SDL_DestroyTexture_f = lib.LoadFunction<SDL_DestroyTexture_d>(nameof(SDL_DestroyTexture));
            SDL_DestroyWindow_f = lib.LoadFunction<SDL_DestroyWindow_d>(nameof(SDL_DestroyWindow));
            SDL_DisableScreenSaver_f = lib.LoadFunction<SDL_DisableScreenSaver_d>(nameof(SDL_DisableScreenSaver));
            SDL_EnableScreenSaver_f = lib.LoadFunction<SDL_EnableScreenSaver_d>(nameof(SDL_EnableScreenSaver));
            SDL_EventState_f = lib.LoadFunction<SDL_EventState_d>(nameof(SDL_EventState));
            SDL_FillRect_f = lib.LoadFunction<SDL_FillRect_d>(nameof(SDL_FillRect));
            SDL_FillRects_f = lib.LoadFunction<SDL_FillRects_d>(nameof(SDL_FillRects));
            SDL_FilterEvents_f = lib.LoadFunction<SDL_FilterEvents_d>(nameof(SDL_FilterEvents));
            SDL_FlushEvent_f = lib.LoadFunction<SDL_FlushEvent_d>(nameof(SDL_FlushEvent));
            SDL_FlushEvents_f = lib.LoadFunction<SDL_FlushEvents_d>(nameof(SDL_FlushEvents));
            SDL_FreeCursor_f = lib.LoadFunction<SDL_FreeCursor_d>(nameof(SDL_FreeCursor));
            SDL_FreeFormat_f = lib.LoadFunction<SDL_FreeFormat_d>(nameof(SDL_FreeFormat));
            SDL_FreePalette_f = lib.LoadFunction<SDL_FreePalette_d>(nameof(SDL_FreePalette));
            SDL_FreeSurface_f = lib.LoadFunction<SDL_FreeSurface_d>(nameof(SDL_FreeSurface));
            SDL_GameControllerAddMapping_f =
                lib.LoadFunction<SDL_GameControllerAddMapping_d>(nameof(SDL_GameControllerAddMapping));
            SDL_GameControllerClose_f = lib.LoadFunction<SDL_GameControllerClose_d>(nameof(SDL_GameControllerClose));
            SDL_GameControllerFromInstanceID_f =
                lib.LoadFunction<SDL_GameControllerFromInstanceID_d>(nameof(SDL_GameControllerFromInstanceID));
            SDL_GameControllerGetAttached_f =
                lib.LoadFunction<SDL_GameControllerGetAttached_d>(nameof(SDL_GameControllerGetAttached));
            SDL_GameControllerGetAxis_f =
                lib.LoadFunction<SDL_GameControllerGetAxis_d>(nameof(SDL_GameControllerGetAxis));
            SDL_GameControllerGetAxisFromString_f =
                lib.LoadFunction<SDL_GameControllerGetAxisFromString_d>(nameof(SDL_GameControllerGetAxisFromString));
            SDL_GameControllerGetBindForAxis_f =
                lib.LoadFunction<SDL_GameControllerGetBindForAxis_d>(nameof(SDL_GameControllerGetBindForAxis));
            SDL_GameControllerGetBindForButton_f =
                lib.LoadFunction<SDL_GameControllerGetBindForButton_d>(nameof(SDL_GameControllerGetBindForButton));
            SDL_GameControllerGetButton_f =
                lib.LoadFunction<SDL_GameControllerGetButton_d>(nameof(SDL_GameControllerGetButton));
            SDL_GameControllerGetButtonFromString_f =
                lib.LoadFunction<SDL_GameControllerGetButtonFromString_d>(
                    nameof(SDL_GameControllerGetButtonFromString));
            SDL_GameControllerGetJoystick_f =
                lib.LoadFunction<SDL_GameControllerGetJoystick_d>(nameof(SDL_GameControllerGetJoystick));
            SDL_GameControllerGetStringForAxis_f =
                lib.LoadFunction<SDL_GameControllerGetStringForAxis_d>(nameof(SDL_GameControllerGetStringForAxis));
            SDL_GameControllerGetStringForButton_f =
                lib.LoadFunction<SDL_GameControllerGetStringForButton_d>(nameof(SDL_GameControllerGetStringForButton));
            SDL_GameControllerMapping_f =
                lib.LoadFunction<SDL_GameControllerMapping_d>(nameof(SDL_GameControllerMapping));
            SDL_GameControllerMappingForGUID_f =
                lib.LoadFunction<SDL_GameControllerMappingForGUID_d>(nameof(SDL_GameControllerMappingForGUID));
            SDL_GameControllerName_f = lib.LoadFunction<SDL_GameControllerName_d>(nameof(SDL_GameControllerName));
            SDL_GameControllerNameForIndex_f =
                lib.LoadFunction<SDL_GameControllerNameForIndex_d>(nameof(SDL_GameControllerNameForIndex));
            SDL_GameControllerOpen_f = lib.LoadFunction<SDL_GameControllerOpen_d>(nameof(SDL_GameControllerOpen));
            SDL_GameControllerUpdate_f = lib.LoadFunction<SDL_GameControllerUpdate_d>(nameof(SDL_GameControllerUpdate));
            SDL_GetClipboardText_f = lib.LoadFunction<SDL_GetClipboardText_d>(nameof(SDL_GetClipboardText));
            SDL_GetClipRect_f = lib.LoadFunction<SDL_GetClipRect_d>(nameof(SDL_GetClipRect));
            SDL_GetClosestDisplayMode_f =
                lib.LoadFunction<SDL_GetClosestDisplayMode_d>(nameof(SDL_GetClosestDisplayMode));
            SDL_GetColorKey_f = lib.LoadFunction<SDL_GetColorKey_d>(nameof(SDL_GetColorKey));
            SDL_GetCurrentDisplayMode_f =
                lib.LoadFunction<SDL_GetCurrentDisplayMode_d>(nameof(SDL_GetCurrentDisplayMode));
            SDL_GetCurrentVideoDriver_f =
                lib.LoadFunction<SDL_GetCurrentVideoDriver_d>(nameof(SDL_GetCurrentVideoDriver));
            SDL_GetCursor_f = lib.LoadFunction<SDL_GetCursor_d>(nameof(SDL_GetCursor));
            SDL_GetDefaultCursor_f = lib.LoadFunction<SDL_GetDefaultCursor_d>(nameof(SDL_GetDefaultCursor));
            SDL_GetDesktopDisplayMode_f =
                lib.LoadFunction<SDL_GetDesktopDisplayMode_d>(nameof(SDL_GetDesktopDisplayMode));
            SDL_GetDisplayBounds_f = lib.LoadFunction<SDL_GetDisplayBounds_d>(nameof(SDL_GetDisplayBounds));
            SDL_GetDisplayDPI_f = lib.LoadFunction<SDL_GetDisplayDPI_d>(nameof(SDL_GetDisplayDPI));
            SDL_GetDisplayMode_f = lib.LoadFunction<SDL_GetDisplayMode_d>(nameof(SDL_GetDisplayMode));
            SDL_GetDisplayName_f = lib.LoadFunction<SDL_GetDisplayName_d>(nameof(SDL_GetDisplayName));
            SDL_GetDisplayUsableBounds_f =
                lib.LoadFunction<SDL_GetDisplayUsableBounds_d>(nameof(SDL_GetDisplayUsableBounds));
            SDL_GetError_f = lib.LoadFunction<SDL_GetError_d>(nameof(SDL_GetError));
            SDL_GetEventFilter_f = lib.LoadFunction<SDL_GetEventFilter_d>(nameof(SDL_GetEventFilter));
            SDL_GetGlobalMouseState_f = lib.LoadFunction<SDL_GetGlobalMouseState_d>(nameof(SDL_GetGlobalMouseState));
            SDL_GetGrabbedWindow_f = lib.LoadFunction<SDL_GetGrabbedWindow_d>(nameof(SDL_GetGrabbedWindow));
            SDL_GetKeyboardFocus_f = lib.LoadFunction<SDL_GetKeyboardFocus_d>(nameof(SDL_GetKeyboardFocus));
            SDL_GetKeyboardState_f = lib.LoadFunction<SDL_GetKeyboardState_d>(nameof(SDL_GetKeyboardState));
            SDL_GetKeyFromName_f = lib.LoadFunction<SDL_GetKeyFromName_d>(nameof(SDL_GetKeyFromName));
            SDL_GetKeyFromScancode_f = lib.LoadFunction<SDL_GetKeyFromScancode_d>(nameof(SDL_GetKeyFromScancode));
            SDL_GetKeyName_f = lib.LoadFunction<SDL_GetKeyName_d>(nameof(SDL_GetKeyName));
            SDL_GetModState_f = lib.LoadFunction<SDL_GetModState_d>(nameof(SDL_GetModState));
            SDL_GetMouseFocus_f = lib.LoadFunction<SDL_GetMouseFocus_d>(nameof(SDL_GetMouseFocus));
            SDL_GetMouseState_f = lib.LoadFunction<SDL_GetMouseState_d>(nameof(SDL_GetMouseState));
            SDL_GetNumDisplayModes_f = lib.LoadFunction<SDL_GetNumDisplayModes_d>(nameof(SDL_GetNumDisplayModes));
            SDL_GetNumRenderDrivers_f = lib.LoadFunction<SDL_GetNumRenderDrivers_d>(nameof(SDL_GetNumRenderDrivers));
            SDL_GetNumTouchDevices_f = lib.LoadFunction<SDL_GetNumTouchDevices_d>(nameof(SDL_GetNumTouchDevices));
            SDL_GetNumTouchFingers_f = lib.LoadFunction<SDL_GetNumTouchFingers_d>(nameof(SDL_GetNumTouchFingers));
            SDL_GetNumVideoDisplays_f = lib.LoadFunction<SDL_GetNumVideoDisplays_d>(nameof(SDL_GetNumVideoDisplays));
            SDL_GetNumVideoDrivers_f = lib.LoadFunction<SDL_GetNumVideoDrivers_d>(nameof(SDL_GetNumVideoDrivers));
            SDL_GetPixelFormatName_f = lib.LoadFunction<SDL_GetPixelFormatName_d>(nameof(SDL_GetPixelFormatName));
            SDL_GetRelativeMouseMode_f = lib.LoadFunction<SDL_GetRelativeMouseMode_d>(nameof(SDL_GetRelativeMouseMode));
            SDL_GetRelativeMouseState_f =
                lib.LoadFunction<SDL_GetRelativeMouseState_d>(nameof(SDL_GetRelativeMouseState));
            SDL_GetRenderDrawBlendMode_f =
                lib.LoadFunction<SDL_GetRenderDrawBlendMode_d>(nameof(SDL_GetRenderDrawBlendMode));
            SDL_GetRenderDrawColor_f = lib.LoadFunction<SDL_GetRenderDrawColor_d>(nameof(SDL_GetRenderDrawColor));
            SDL_GetRenderDriverInfo_f = lib.LoadFunction<SDL_GetRenderDriverInfo_d>(nameof(SDL_GetRenderDriverInfo));
            SDL_GetRenderer_f = lib.LoadFunction<SDL_GetRenderer_d>(nameof(SDL_GetRenderer));
            SDL_GetRendererInfo_f = lib.LoadFunction<SDL_GetRendererInfo_d>(nameof(SDL_GetRendererInfo));
            SDL_GetRendererOutputSize_f =
                lib.LoadFunction<SDL_GetRendererOutputSize_d>(nameof(SDL_GetRendererOutputSize));
            SDL_GetRenderTarget_f = lib.LoadFunction<SDL_GetRenderTarget_d>(nameof(SDL_GetRenderTarget));
            SDL_GetRevision_f = lib.LoadFunction<SDL_GetRevision_d>(nameof(SDL_GetRevision));
            SDL_GetRevisionNumber_f = lib.LoadFunction<SDL_GetRevisionNumber_d>(nameof(SDL_GetRevisionNumber));
            SDL_GetRGB_f = lib.LoadFunction<SDL_GetRGB_d>(nameof(SDL_GetRGB));
            SDL_GetRGBA_f = lib.LoadFunction<SDL_GetRGBA_d>(nameof(SDL_GetRGBA));
            SDL_GetScancodeFromKey_f = lib.LoadFunction<SDL_GetScancodeFromKey_d>(nameof(SDL_GetScancodeFromKey));
            SDL_GetScancodeFromName_f = lib.LoadFunction<SDL_GetScancodeFromName_d>(nameof(SDL_GetScancodeFromName));
            SDL_GetScancodeName_f = lib.LoadFunction<SDL_GetScancodeName_d>(nameof(SDL_GetScancodeName));
            SDL_GetSurfaceAlphaMod_f = lib.LoadFunction<SDL_GetSurfaceAlphaMod_d>(nameof(SDL_GetSurfaceAlphaMod));
            SDL_GetSurfaceBlendMode_f = lib.LoadFunction<SDL_GetSurfaceBlendMode_d>(nameof(SDL_GetSurfaceBlendMode));
            SDL_GetSurfaceColorMod_f = lib.LoadFunction<SDL_GetSurfaceColorMod_d>(nameof(SDL_GetSurfaceColorMod));
            SDL_GetTextureAlphaMod_f = lib.LoadFunction<SDL_GetTextureAlphaMod_d>(nameof(SDL_GetTextureAlphaMod));
            SDL_GetTextureBlendMode_f = lib.LoadFunction<SDL_GetTextureBlendMode_d>(nameof(SDL_GetTextureBlendMode));
            SDL_GetTextureColorMod_f = lib.LoadFunction<SDL_GetTextureColorMod_d>(nameof(SDL_GetTextureColorMod));
            SDL_GetTouchDevice_f = lib.LoadFunction<SDL_GetTouchDevice_d>(nameof(SDL_GetTouchDevice));
            SDL_GetTouchFinger_f = lib.LoadFunction<SDL_GetTouchFinger_d>(nameof(SDL_GetTouchFinger));
            SDL_GetVersion_f = lib.LoadFunction<SDL_GetVersion_d>(nameof(SDL_GetVersion));
            SDL_GetVideoDriver_f = lib.LoadFunction<SDL_GetVideoDriver_d>(nameof(SDL_GetVideoDriver));
            SDL_GetWindowBordersSize_f = lib.LoadFunction<SDL_GetWindowBordersSize_d>(nameof(SDL_GetWindowBordersSize));
            SDL_GetWindowID_f = lib.LoadFunction<SDL_GetWindowID_d>(nameof(SDL_GetWindowID));
            SDL_GetWindowBrightness_f = lib.LoadFunction<SDL_GetWindowBrightness_d>(nameof(SDL_GetWindowBrightness));
            SDL_GetWindowData_f = lib.LoadFunction<SDL_GetWindowData_d>(nameof(SDL_GetWindowData));
            SDL_GetWindowDisplayIndex_f =
                lib.LoadFunction<SDL_GetWindowDisplayIndex_d>(nameof(SDL_GetWindowDisplayIndex));
            SDL_GetWindowDisplayMode_f = lib.LoadFunction<SDL_GetWindowDisplayMode_d>(nameof(SDL_GetWindowDisplayMode));
            SDL_GetWindowFlags_f = lib.LoadFunction<SDL_GetWindowFlags_d>(nameof(SDL_GetWindowFlags));
            SDL_GetWindowFromID_f = lib.LoadFunction<SDL_GetWindowFromID_d>(nameof(SDL_GetWindowFromID));
            SDL_GetWindowMaximumSize_f = lib.LoadFunction<SDL_GetWindowMaximumSize_d>(nameof(SDL_GetWindowMaximumSize));
            SDL_GetWindowMinimumSize_f = lib.LoadFunction<SDL_GetWindowMinimumSize_d>(nameof(SDL_GetWindowMinimumSize));
            SDL_GetWindowOpacity_f = lib.LoadFunction<SDL_GetWindowOpacity_d>(nameof(SDL_GetWindowOpacity));
            SDL_GetWindowPixelFormat_f = lib.LoadFunction<SDL_GetWindowPixelFormat_d>(nameof(SDL_GetWindowPixelFormat));
            SDL_GetWindowPosition_f = lib.LoadFunction<SDL_GetWindowPosition_d>(nameof(SDL_GetWindowPosition));
            SDL_GetWindowSize_f = lib.LoadFunction<SDL_GetWindowSize_d>(nameof(SDL_GetWindowSize));
            SDL_GetWindowTitle_f = lib.LoadFunction<SDL_GetWindowTitle_d>(nameof(SDL_GetWindowTitle));
            SDL_GetWindowWMInfo_f = lib.LoadFunction<SDL_GetWindowWMInfo_d>(nameof(SDL_GetWindowWMInfo));
            SDL_GL_BindTexture_f = lib.LoadFunction<SDL_GL_BindTexture_d>(nameof(SDL_GL_BindTexture));
            SDL_GL_CreateContext_f = lib.LoadFunction<SDL_GL_CreateContext_d>(nameof(SDL_GL_CreateContext));
            SDL_GL_DeleteContext_f = lib.LoadFunction<SDL_GL_DeleteContext_d>(nameof(SDL_GL_DeleteContext));
            SDL_GL_ExtensionSupported_f =
                lib.LoadFunction<SDL_GL_ExtensionSupported_d>(nameof(SDL_GL_ExtensionSupported));
            SDL_GL_GetAttribute_f = lib.LoadFunction<SDL_GL_GetAttribute_d>(nameof(SDL_GL_GetAttribute));
            SDL_GL_GetCurrentContext_f = lib.LoadFunction<SDL_GL_GetCurrentContext_d>(nameof(SDL_GL_GetCurrentContext));
            SDL_GL_GetCurrentWindow_f = lib.LoadFunction<SDL_GL_GetCurrentWindow_d>(nameof(SDL_GL_GetCurrentWindow));
            SDL_GL_GetDrawableSize_f = lib.LoadFunction<SDL_GL_GetDrawableSize_d>(nameof(SDL_GL_GetDrawableSize));
            SDL_GL_GetProcAddress_f = lib.LoadFunction<SDL_GL_GetProcAddress_d>(nameof(SDL_GL_GetProcAddress));
            SDL_GL_GetSwapInterval_f = lib.LoadFunction<SDL_GL_GetSwapInterval_d>(nameof(SDL_GL_GetSwapInterval));
            SDL_GL_LoadLibrary_f = lib.LoadFunction<SDL_GL_LoadLibrary_d>(nameof(SDL_GL_LoadLibrary));
            SDL_GL_MakeCurrent_f = lib.LoadFunction<SDL_GL_MakeCurrent_d>(nameof(SDL_GL_MakeCurrent));
            SDL_GL_ResetAttributes_f = lib.LoadFunction<SDL_GL_ResetAttributes_d>(nameof(SDL_GL_ResetAttributes));
            SDL_GL_SetAttribute_f = lib.LoadFunction<SDL_GL_SetAttribute_d>(nameof(SDL_GL_SetAttribute));
            SDL_GL_SetSwapInterval_f = lib.LoadFunction<SDL_GL_SetSwapInterval_d>(nameof(SDL_GL_SetSwapInterval));
            SDL_GL_SwapWindow_f = lib.LoadFunction<SDL_GL_SwapWindow_d>(nameof(SDL_GL_SwapWindow));
            SDL_GL_UnbindTexture_f = lib.LoadFunction<SDL_GL_UnbindTexture_d>(nameof(SDL_GL_UnbindTexture));
            SDL_GL_UnloadLibrary_f = lib.LoadFunction<SDL_GL_UnloadLibrary_d>(nameof(SDL_GL_UnloadLibrary));
            SDL_HasClipboardText_f = lib.LoadFunction<SDL_HasClipboardText_d>(nameof(SDL_HasClipboardText));
            SDL_HasEvent_f = lib.LoadFunction<SDL_HasEvent_d>(nameof(SDL_HasEvent));
            SDL_HasEvents_f = lib.LoadFunction<SDL_HasEvents_d>(nameof(SDL_HasEvents));
            SDL_HasScreenKeyboardSupport_f =
                lib.LoadFunction<SDL_HasScreenKeyboardSupport_d>(nameof(SDL_HasScreenKeyboardSupport));
            SDL_HideWindow_f = lib.LoadFunction<SDL_HideWindow_d>(nameof(SDL_HideWindow));
            SDL_Init_f = lib.LoadFunction<SDL_Init_d>(nameof(SDL_Init));
            SDL_InitSubSystem_f = lib.LoadFunction<SDL_InitSubSystem_d>(nameof(SDL_InitSubSystem));
            SDL_IsGameController_f = lib.LoadFunction<SDL_IsGameController_d>(nameof(SDL_IsGameController));
            SDL_IsScreenKeyboardShown_f =
                lib.LoadFunction<SDL_IsScreenKeyboardShown_d>(nameof(SDL_IsScreenKeyboardShown));
            SDL_IsScreenSaverEnabled_f = lib.LoadFunction<SDL_IsScreenSaverEnabled_d>(nameof(SDL_IsScreenSaverEnabled));
            SDL_IsTextInputActive_f = lib.LoadFunction<SDL_IsTextInputActive_d>(nameof(SDL_IsTextInputActive));
            SDL_JoystickClose_f = lib.LoadFunction<SDL_JoystickClose_d>(nameof(SDL_JoystickClose));
            SDL_JoystickCurrentPowerLevel_f =
                lib.LoadFunction<SDL_JoystickCurrentPowerLevel_d>(nameof(SDL_JoystickCurrentPowerLevel));
            SDL_JoystickFromInstanceID_f =
                lib.LoadFunction<SDL_JoystickFromInstanceID_d>(nameof(SDL_JoystickFromInstanceID));
            SDL_JoystickGetAttached_f = lib.LoadFunction<SDL_JoystickGetAttached_d>(nameof(SDL_JoystickGetAttached));
            SDL_JoystickGetAxis_f = lib.LoadFunction<SDL_JoystickGetAxis_d>(nameof(SDL_JoystickGetAxis));
            SDL_JoystickGetBall_f = lib.LoadFunction<SDL_JoystickGetBall_d>(nameof(SDL_JoystickGetBall));
            SDL_JoystickGetButton_f = lib.LoadFunction<SDL_JoystickGetButton_d>(nameof(SDL_JoystickGetButton));
            SDL_JoystickGetDeviceGUID_f =
                lib.LoadFunction<SDL_JoystickGetDeviceGUID_d>(nameof(SDL_JoystickGetDeviceGUID));
            SDL_JoystickGetGUID_f = lib.LoadFunction<SDL_JoystickGetGUID_d>(nameof(SDL_JoystickGetGUID));
            SDL_JoystickGetGUIDFromString_f =
                lib.LoadFunction<SDL_JoystickGetGUIDFromString_d>(nameof(SDL_JoystickGetGUIDFromString));
            SDL_JoystickGetGUIDString_f =
                lib.LoadFunction<SDL_JoystickGetGUIDString_d>(nameof(SDL_JoystickGetGUIDString));
            SDL_JoystickGetHat_f = lib.LoadFunction<SDL_JoystickGetHat_d>(nameof(SDL_JoystickGetHat));
            SDL_JoystickInstanceID_f = lib.LoadFunction<SDL_JoystickInstanceID_d>(nameof(SDL_JoystickInstanceID));
            SDL_JoystickName_f = lib.LoadFunction<SDL_JoystickName_d>(nameof(SDL_JoystickName));
            SDL_JoystickNameForIndex_f = lib.LoadFunction<SDL_JoystickNameForIndex_d>(nameof(SDL_JoystickNameForIndex));
            SDL_JoystickNumAxes_f = lib.LoadFunction<SDL_JoystickNumAxes_d>(nameof(SDL_JoystickNumAxes));
            SDL_JoystickNumBalls_f = lib.LoadFunction<SDL_JoystickNumBalls_d>(nameof(SDL_JoystickNumBalls));
            SDL_JoystickNumButtons_f = lib.LoadFunction<SDL_JoystickNumButtons_d>(nameof(SDL_JoystickNumButtons));
            SDL_JoystickNumHats_f = lib.LoadFunction<SDL_JoystickNumHats_d>(nameof(SDL_JoystickNumHats));
            SDL_JoystickOpen_f = lib.LoadFunction<SDL_JoystickOpen_d>(nameof(SDL_JoystickOpen));
            SDL_JoystickUpdate_f = lib.LoadFunction<SDL_JoystickUpdate_d>(nameof(SDL_JoystickUpdate));
            SDL_LockSurface_f = lib.LoadFunction<SDL_LockSurface_d>(nameof(SDL_LockSurface));
            SDL_LockTexture_f = lib.LoadFunction<SDL_LockTexture_d>(nameof(SDL_LockTexture));
            SDL_LowerBlit_f = lib.LoadFunction<SDL_LowerBlit_d>(nameof(SDL_LowerBlit));
            SDL_LowerBlitScaled_f = lib.LoadFunction<SDL_LowerBlitScaled_d>(nameof(SDL_LowerBlitScaled));
            SDL_MapRGB_f = lib.LoadFunction<SDL_MapRGB_d>(nameof(SDL_MapRGB));
            SDL_MapRGBA_f = lib.LoadFunction<SDL_MapRGBA_d>(nameof(SDL_MapRGBA));
            SDL_MasksToPixelFormatEnum_f =
                lib.LoadFunction<SDL_MasksToPixelFormatEnum_d>(nameof(SDL_MasksToPixelFormatEnum));
            SDL_MaximizeWindow_f = lib.LoadFunction<SDL_MaximizeWindow_d>(nameof(SDL_MaximizeWindow));
            SDL_MinimizeWindow_f = lib.LoadFunction<SDL_MinimizeWindow_d>(nameof(SDL_MinimizeWindow));
            SDL_NumJoysticks_f = lib.LoadFunction<SDL_NumJoysticks_d>(nameof(SDL_NumJoysticks));
            SDL_PeepEvents_f = lib.LoadFunction<SDL_PeepEvents_d>(nameof(SDL_PeepEvents));
            SDL_PixelFormatEnumToMasks_f =
                lib.LoadFunction<SDL_PixelFormatEnumToMasks_d>(nameof(SDL_PixelFormatEnumToMasks));
            SDL_PollEvent_f = lib.LoadFunction<SDL_PollEvent_d>(nameof(SDL_PollEvent));
            SDL_PumpEvents_f = lib.LoadFunction<SDL_PumpEvents_d>(nameof(SDL_PumpEvents));
            SDL_PushEvent_f = lib.LoadFunction<SDL_PushEvent_d>(nameof(SDL_PushEvent));
            SDL_QueryTexture_f = lib.LoadFunction<SDL_QueryTexture_d>(nameof(SDL_QueryTexture));
            SDL_Quit_f = lib.LoadFunction<SDL_Quit_d>(nameof(SDL_Quit));
            SDL_QuitSubSystem_f = lib.LoadFunction<SDL_QuitSubSystem_d>(nameof(SDL_QuitSubSystem));
            SDL_RaiseWindow_f = lib.LoadFunction<SDL_RaiseWindow_d>(nameof(SDL_RaiseWindow));
            SDL_RegisterEvents_f = lib.LoadFunction<SDL_RegisterEvents_d>(nameof(SDL_RegisterEvents));
            SDL_RenderClear_f = lib.LoadFunction<SDL_RenderClear_d>(nameof(SDL_RenderClear));
            SDL_RenderCopy_f = lib.LoadFunction<SDL_RenderCopy_d>(nameof(SDL_RenderCopy));
            SDL_RenderCopyEx_f = lib.LoadFunction<SDL_RenderCopyEx_d>(nameof(SDL_RenderCopyEx));
            SDL_RenderDrawLine_f = lib.LoadFunction<SDL_RenderDrawLine_d>(nameof(SDL_RenderDrawLine));
            SDL_RenderDrawLines_f = lib.LoadFunction<SDL_RenderDrawLines_d>(nameof(SDL_RenderDrawLines));
            SDL_RenderDrawPoint_f = lib.LoadFunction<SDL_RenderDrawPoint_d>(nameof(SDL_RenderDrawPoint));
            SDL_RenderDrawPoints_f = lib.LoadFunction<SDL_RenderDrawPoints_d>(nameof(SDL_RenderDrawPoints));
            SDL_RenderDrawRect_f = lib.LoadFunction<SDL_RenderDrawRect_d>(nameof(SDL_RenderDrawRect));
            SDL_RenderDrawRects_f = lib.LoadFunction<SDL_RenderDrawRects_d>(nameof(SDL_RenderDrawRects));
            SDL_RenderFillRect_f = lib.LoadFunction<SDL_RenderFillRect_d>(nameof(SDL_RenderFillRect));
            SDL_RenderFillRects_f = lib.LoadFunction<SDL_RenderFillRects_d>(nameof(SDL_RenderFillRects));
            SDL_RenderGetClipRect_f = lib.LoadFunction<SDL_RenderGetClipRect_d>(nameof(SDL_RenderGetClipRect));
            SDL_RenderGetIntegerScale_f =
                lib.LoadFunction<SDL_RenderGetIntegerScale_d>(nameof(SDL_RenderGetIntegerScale));
            SDL_RenderGetLogicalSize_f = lib.LoadFunction<SDL_RenderGetLogicalSize_d>(nameof(SDL_RenderGetLogicalSize));
            SDL_RenderGetScale_f = lib.LoadFunction<SDL_RenderGetScale_d>(nameof(SDL_RenderGetScale));
            SDL_RenderGetViewport_f = lib.LoadFunction<SDL_RenderGetViewport_d>(nameof(SDL_RenderGetViewport));
            SDL_RenderIsClipEnabled_f = lib.LoadFunction<SDL_RenderIsClipEnabled_d>(nameof(SDL_RenderIsClipEnabled));
            SDL_RenderPresent_f = lib.LoadFunction<SDL_RenderPresent_d>(nameof(SDL_RenderPresent));
            SDL_RenderReadPixels_f = lib.LoadFunction<SDL_RenderReadPixels_d>(nameof(SDL_RenderReadPixels));
            SDL_RenderSetClipRect_f = lib.LoadFunction<SDL_RenderSetClipRect_d>(nameof(SDL_RenderSetClipRect));
            SDL_RenderSetIntegerScale_f =
                lib.LoadFunction<SDL_RenderSetIntegerScale_d>(nameof(SDL_RenderSetIntegerScale));
            SDL_RenderSetLogicalSize_f = lib.LoadFunction<SDL_RenderSetLogicalSize_d>(nameof(SDL_RenderSetLogicalSize));
            SDL_RenderSetScale_f = lib.LoadFunction<SDL_RenderSetScale_d>(nameof(SDL_RenderSetScale));
            SDL_RenderSetViewport_f = lib.LoadFunction<SDL_RenderSetViewport_d>(nameof(SDL_RenderSetViewport));
            SDL_RenderTargetSupported_f =
                lib.LoadFunction<SDL_RenderTargetSupported_d>(nameof(SDL_RenderTargetSupported));
            SDL_RestoreWindow_f = lib.LoadFunction<SDL_RestoreWindow_d>(nameof(SDL_RestoreWindow));
            SDL_SetClipboardText_f = lib.LoadFunction<SDL_SetClipboardText_d>(nameof(SDL_SetClipboardText));
            SDL_SetClipRect_f = lib.LoadFunction<SDL_SetClipRect_d>(nameof(SDL_SetClipRect));
            SDL_SetColorKey_f = lib.LoadFunction<SDL_SetColorKey_d>(nameof(SDL_SetColorKey));
            SDL_SetCursor_f = lib.LoadFunction<SDL_SetCursor_d>(nameof(SDL_SetCursor));
            SDL_SetEventFilter_f = lib.LoadFunction<SDL_SetEventFilter_d>(nameof(SDL_SetEventFilter));
            SDL_SetModState_f = lib.LoadFunction<SDL_SetModState_d>(nameof(SDL_SetModState));
            SDL_SetPaletteColors_f = lib.LoadFunction<SDL_SetPaletteColors_d>(nameof(SDL_SetPaletteColors));
            SDL_SetPixelFormatPalette_f =
                lib.LoadFunction<SDL_SetPixelFormatPalette_d>(nameof(SDL_SetPixelFormatPalette));
            SDL_SetRelativeMouseMode_f = lib.LoadFunction<SDL_SetRelativeMouseMode_d>(nameof(SDL_SetRelativeMouseMode));
            SDL_SetRenderDrawBlendMode_f =
                lib.LoadFunction<SDL_SetRenderDrawBlendMode_d>(nameof(SDL_SetRenderDrawBlendMode));
            SDL_SetRenderDrawColor_f = lib.LoadFunction<SDL_SetRenderDrawColor_d>(nameof(SDL_SetRenderDrawColor));
            SDL_SetRenderTarget_f = lib.LoadFunction<SDL_SetRenderTarget_d>(nameof(SDL_SetRenderTarget));
            SDL_SetSurfaceAlphaMod_f = lib.LoadFunction<SDL_SetSurfaceAlphaMod_d>(nameof(SDL_SetSurfaceAlphaMod));
            SDL_SetSurfaceBlendMode_f = lib.LoadFunction<SDL_SetSurfaceBlendMode_d>(nameof(SDL_SetSurfaceBlendMode));
            SDL_SetSurfaceColorMod_f = lib.LoadFunction<SDL_SetSurfaceColorMod_d>(nameof(SDL_SetSurfaceColorMod));
            SDL_SetSurfacePalette_f = lib.LoadFunction<SDL_SetSurfacePalette_d>(nameof(SDL_SetSurfacePalette));
            SDL_SetSurfaceRLE_f = lib.LoadFunction<SDL_SetSurfaceRLE_d>(nameof(SDL_SetSurfaceRLE));
            SDL_SetTextInputRect_f = lib.LoadFunction<SDL_SetTextInputRect_d>(nameof(SDL_SetTextInputRect));
            SDL_SetTextureAlphaMod_f = lib.LoadFunction<SDL_SetTextureAlphaMod_d>(nameof(SDL_SetTextureAlphaMod));
            SDL_SetTextureBlendMode_f = lib.LoadFunction<SDL_SetTextureBlendMode_d>(nameof(SDL_SetTextureBlendMode));
            SDL_SetTextureColorMod_f = lib.LoadFunction<SDL_SetTextureColorMod_d>(nameof(SDL_SetTextureColorMod));
            SDL_SetWindowBordered_f = lib.LoadFunction<SDL_SetWindowBordered_d>(nameof(SDL_SetWindowBordered));
            SDL_SetWindowBrightness_f = lib.LoadFunction<SDL_SetWindowBrightness_d>(nameof(SDL_SetWindowBrightness));
            SDL_SetWindowData_f = lib.LoadFunction<SDL_SetWindowData_d>(nameof(SDL_SetWindowData));
            SDL_SetWindowDisplayMode_f = lib.LoadFunction<SDL_SetWindowDisplayMode_d>(nameof(SDL_SetWindowDisplayMode));
            SDL_SetWindowFullscreen_f = lib.LoadFunction<SDL_SetWindowFullscreen_d>(nameof(SDL_SetWindowFullscreen));
            SDL_SetWindowGammaRamp_f = lib.LoadFunction<SDL_SetWindowGammaRamp_d>(nameof(SDL_SetWindowGammaRamp));
            SDL_SetWindowGrab_f = lib.LoadFunction<SDL_SetWindowGrab_d>(nameof(SDL_SetWindowGrab));
            SDL_SetWindowHitTest_f = lib.LoadFunction<SDL_SetWindowHitTest_d>(nameof(SDL_SetWindowHitTest));
            SDL_SetWindowInputFocus_f = lib.LoadFunction<SDL_SetWindowInputFocus_d>(nameof(SDL_SetWindowInputFocus));
            SDL_SetWindowMaximumSize_f = lib.LoadFunction<SDL_SetWindowMaximumSize_d>(nameof(SDL_SetWindowMaximumSize));
            SDL_SetWindowMinimumSize_f = lib.LoadFunction<SDL_SetWindowMinimumSize_d>(nameof(SDL_SetWindowMinimumSize));
            SDL_SetWindowModalFor_f = lib.LoadFunction<SDL_SetWindowModalFor_d>(nameof(SDL_SetWindowModalFor));
            SDL_SetWindowOpacity_f = lib.LoadFunction<SDL_SetWindowOpacity_d>(nameof(SDL_SetWindowOpacity));
            SDL_SetWindowPosition_f = lib.LoadFunction<SDL_SetWindowPosition_d>(nameof(SDL_SetWindowPosition));
            SDL_SetWindowResizable_f = lib.LoadFunction<SDL_SetWindowResizable_d>(nameof(SDL_SetWindowResizable));
            SDL_SetWindowSize_f = lib.LoadFunction<SDL_SetWindowSize_d>(nameof(SDL_SetWindowSize));
            SDL_SetWindowTitle_f = lib.LoadFunction<SDL_SetWindowTitle_d>(nameof(SDL_SetWindowTitle));
            SDL_ShowCursor_f = lib.LoadFunction<SDL_ShowCursor_d>(nameof(SDL_ShowCursor));
            SDL_ShowWindow_f = lib.LoadFunction<SDL_ShowWindow_d>(nameof(SDL_ShowWindow));
            SDL_StartTextInput_f = lib.LoadFunction<SDL_StartTextInput_d>(nameof(SDL_StartTextInput));
            SDL_StopTextInput_f = lib.LoadFunction<SDL_StopTextInput_d>(nameof(SDL_StopTextInput));
            SDL_UnlockSurface_f = lib.LoadFunction<SDL_UnlockSurface_d>(nameof(SDL_UnlockSurface));
            SDL_UnlockTexture_f = lib.LoadFunction<SDL_UnlockTexture_d>(nameof(SDL_UnlockTexture));
            SDL_UpdateTexture_f = lib.LoadFunction<SDL_UpdateTexture_d>(nameof(SDL_UpdateTexture));
            SDL_UpdateYUVTexture_f = lib.LoadFunction<SDL_UpdateYUVTexture_d>(nameof(SDL_UpdateYUVTexture));
            SDL_UpperBlit_f = lib.LoadFunction<SDL_UpperBlit_d>(nameof(SDL_UpperBlit));
            SDL_UpperBlitScaled_f = lib.LoadFunction<SDL_UpperBlitScaled_d>(nameof(SDL_UpperBlitScaled));
            SDL_VideoInit_f = lib.LoadFunction<SDL_VideoInit_d>(nameof(SDL_VideoInit));
            SDL_VideoQuit_f = lib.LoadFunction<SDL_VideoQuit_d>(nameof(SDL_VideoQuit));
            SDL_WaitEvent_f = lib.LoadFunction<SDL_WaitEvent_d>(nameof(SDL_WaitEvent));
            SDL_WaitEventTimeout_f = lib.LoadFunction<SDL_WaitEventTimeout_d>(nameof(SDL_WaitEventTimeout));
            SDL_WarpMouseGlobal_f = lib.LoadFunction<SDL_WarpMouseGlobal_d>(nameof(SDL_WarpMouseGlobal));
            SDL_WarpMouseInWindow_f = lib.LoadFunction<SDL_WarpMouseInWindow_d>(nameof(SDL_WarpMouseInWindow));
            SDL_WasInit_f = lib.LoadFunction<SDL_WasInit_d>(nameof(SDL_WasInit));

            return lib;
        }

        /* ENUMS */

        [Flags]
        public enum SDL_InitFlags : uint
        {
            SDL_INIT_NONE = 0,
            SDL_INIT_TIMER = 0x00000001u,
            SDL_INIT_AUDIO = 0x00000010u,
            SDL_INIT_VIDEO = 0x00000020u,
            SDL_INIT_JOYSTICK = 0x00000200u,
            SDL_INIT_HAPTIC = 0x00001000u,
            SDL_INIT_GAMECONTROLLER = 0x00002000u,
            SDL_INIT_EVENTS = 0x00004000u,
            SDL_INIT_NOPARACHUTE = 0x00100000u,

            SDL_INIT_EVERYTHING = SDL_INIT_TIMER | SDL_INIT_AUDIO | SDL_INIT_VIDEO | SDL_INIT_EVENTS
                                  | SDL_INIT_JOYSTICK | SDL_INIT_HAPTIC | SDL_INIT_GAMECONTROLLER
        }

        [Flags]
        public enum SDL_WindowFlags : uint
        {
            SDL_WINDOW_FULLSCREEN = 0x00000001,
            SDL_WINDOW_OPENGL = 0x00000002,
            SDL_WINDOW_SHOWN = 0x00000004,
            SDL_WINDOW_HIDDEN = 0x00000008,
            SDL_WINDOW_BORDERLESS = 0x00000010,
            SDL_WINDOW_RESIZABLE = 0x00000020,
            SDL_WINDOW_MINIMIZED = 0x00000040,
            SDL_WINDOW_MAXIMIZED = 0x00000080,
            SDL_WINDOW_INPUT_GRABBED = 0x00000100,
            SDL_WINDOW_INPUT_FOCUS = 0x00000200,
            SDL_WINDOW_MOUSE_FOCUS = 0x00000400,
            SDL_WINDOW_FULLSCREEN_DESKTOP = (SDL_WINDOW_FULLSCREEN | 0x00001000),
            SDL_WINDOW_FOREIGN = 0x00000800,
            SDL_WINDOW_ALLOW_HIGHDPI = 0x00002000,
            SDL_WINDOW_MOUSE_CAPTURE = 0x00004000,
            SDL_WINDOW_ALWAYS_ON_TOP = 0x00008000,
            SDL_WINDOW_SKIP_TASKBAR = 0x00010000,
            SDL_WINDOW_UTILITY = 0x00020000,
            SDL_WINDOW_TOOLTIP = 0x00040000,
            SDL_WINDOW_POPUP_MENU = 0x00080000,
            SDL_WINDOW_VULKAN = 0x10000000
        }

        public enum SDL_HitTestResult
        {
            SDL_HITTEST_NORMAL,
            SDL_HITTEST_DRAGGABLE,
            SDL_HITTEST_RESIZE_TOPLEFT,
            SDL_HITTEST_RESIZE_TOP,
            SDL_HITTEST_RESIZE_TOPRIGHT,
            SDL_HITTEST_RESIZE_RIGHT,
            SDL_HITTEST_RESIZE_BOTTOMRIGHT,
            SDL_HITTEST_RESIZE_BOTTOM,
            SDL_HITTEST_RESIZE_BOTTOMLEFT,
            SDL_HITTEST_RESIZE_LEFT
        }

        public enum SDL_WindowEventID : byte
        {
            SDL_WINDOWEVENT_NONE,
            SDL_WINDOWEVENT_SHOWN,
            SDL_WINDOWEVENT_HIDDEN,
            SDL_WINDOWEVENT_EXPOSED,
            SDL_WINDOWEVENT_MOVED,
            SDL_WINDOWEVENT_RESIZED,
            SDL_WINDOWEVENT_SIZE_CHANGED,
            SDL_WINDOWEVENT_MINIMIZED,
            SDL_WINDOWEVENT_MAXIMIZED,
            SDL_WINDOWEVENT_RESTORED,
            SDL_WINDOWEVENT_ENTER,
            SDL_WINDOWEVENT_LEAVE,
            SDL_WINDOWEVENT_FOCUS_GAINED,
            SDL_WINDOWEVENT_FOCUS_LOST,
            SDL_WINDOWEVENT_CLOSE,
            SDL_WINDOWEVENT_TAKE_FOCUS,
            SDL_WINDOWEVENT_HIT_TEST
        }

        public enum SDL_GLprofile
        {
            SDL_GL_CONTEXT_PROFILE_CORE = 0x0001,
            SDL_GL_CONTEXT_PROFILE_COMPATIBILITY = 0x0002,
            SDL_GL_CONTEXT_PROFILE_ES = 0x0004 /**< GLX_CONTEXT_ES2_PROFILE_BIT_EXT */
        }

        public enum SDL_GLcontextFlag
        {
            SDL_GL_CONTEXT_DEBUG_FLAG = 0x0001,
            SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG = 0x0002,
            SDL_GL_CONTEXT_ROBUST_ACCESS_FLAG = 0x0004,
            SDL_GL_CONTEXT_RESET_ISOLATION_FLAG = 0x0008
        }

        public enum SDL_GLcontextReleaseBehaviour
        {
            SDL_GL_CONTEXT_RELEASE_BEHAVIOR_NONE = 0x0000,
            SDL_GL_CONTEXT_RELEASE_BEHAVIOR_FLUSH = 0x0001
        }

        public enum SDL_GLContextResetNotification
        {
            SDL_GL_CONTEXT_RESET_NO_NOTIFICATION = 0x0000,
            SDL_GL_CONTEXT_RESET_LOSE_CONTEXT = 0x0001
        }

        public enum SDL_GLattr
        {
            SDL_GL_RED_SIZE,
            SDL_GL_GREEN_SIZE,
            SDL_GL_BLUE_SIZE,
            SDL_GL_ALPHA_SIZE,
            SDL_GL_BUFFER_SIZE,
            SDL_GL_DOUBLEBUFFER,
            SDL_GL_DEPTH_SIZE,
            SDL_GL_STENCIL_SIZE,
            SDL_GL_ACCUM_RED_SIZE,
            SDL_GL_ACCUM_GREEN_SIZE,
            SDL_GL_ACCUM_BLUE_SIZE,
            SDL_GL_ACCUM_ALPHA_SIZE,
            SDL_GL_STEREO,
            SDL_GL_MULTISAMPLEBUFFERS,
            SDL_GL_MULTISAMPLESAMPLES,
            SDL_GL_ACCELERATED_VISUAL,
            SDL_GL_RETAINED_BACKING,
            SDL_GL_CONTEXT_MAJOR_VERSION,
            SDL_GL_CONTEXT_MINOR_VERSION,
            SDL_GL_CONTEXT_EGL,
            SDL_GL_CONTEXT_FLAGS,
            SDL_GL_CONTEXT_PROFILE_MASK,
            SDL_GL_SHARE_WITH_CURRENT_CONTEXT,
            SDL_GL_FRAMEBUFFER_SRGB_CAPABLE,
            SDL_GL_CONTEXT_RELEASE_BEHAVIOR,
            SDL_GL_CONTEXT_RESET_NOTIFICATION,
            SDL_GL_CONTEXT_NO_ERROR
        }


        [Flags]
        public enum SDL_RendererFlags : uint
        {
            SDL_RENDERER_SOFTWARE = 0x00000001,
            SDL_RENDERER_ACCELERATED = 0x00000002,
            SDL_RENDERER_PRESENTVSYNC = 0x00000004,
            SDL_RENDERER_TARGETTEXTURE = 0x00000008
        }

        public enum SDL_TextureAccess
        {
            SDL_TEXTUREACCESS_STATIC,
            SDL_TEXTUREACCESS_STREAMING,
            SDL_TEXTUREACCESS_TARGET
        }

        [Flags]
        public enum SDL_TextureModulate : uint
        {
            SDL_TEXTUREMODULATE_NONE = 0x00000000,
            SDL_TEXTUREMODULATE_COLOR = 0x00000001,
            SDL_TEXTUREMODULATE_ALPHA = 0x00000002
        }

        [Flags]
        public enum SDL_RendererFlip : uint
        {
            SDL_FLIP_NONE = 0x00000000,
            SDL_FLIP_HORIZONTAL = 0x00000001,
            SDL_FLIP_VERTICAL = 0x00000002
        }

        public enum SDL_BlendMode
        {
            SDL_BLENDMODE_NONE = 0x00000000,
            SDL_BLENDMODE_BLEND = 0x00000001,
            SDL_BLENDMODE_ADD = 0x00000002,
            SDL_BLENDMODE_MOD = 0x00000004,
            SDL_BLENDMODE_INVALID = 0x7FFFFFFF
        }

        public enum SDL_BlendOperation
        {
            SDL_BLENDOPERATION_ADD = 0x1,
            SDL_BLENDOPERATION_SUBTRACT = 0x2,
            SDL_BLENDOPERATION_REV_SUBTRACT = 0x3,
            SDL_BLENDOPERATION_MINIMUM = 0x4,
            SDL_BLENDOPERATION_MAXIMUM = 0x5
        }

        public enum SDL_BlendFactor
        {
            SDL_BLENDFACTOR_ZERO = 0x1,
            SDL_BLENDFACTOR_ONE = 0x2,
            SDL_BLENDFACTOR_SRC_COLOR = 0x3,
            SDL_BLENDFACTOR_ONE_MINUS_SRC_COLOR = 0x4,
            SDL_BLENDFACTOR_SRC_ALPHA = 0x5,
            SDL_BLENDFACTOR_ONE_MINUS_SRC_ALPHA = 0x6,
            SDL_BLENDFACTOR_DST_COLOR = 0x7,
            SDL_BLENDFACTOR_ONE_MINUS_DST_COLOR = 0x8,
            SDL_BLENDFACTOR_DST_ALPHA = 0x9,
            SDL_BLENDFACTOR_ONE_MINUS_DST_ALPHA = 0xA
        }

        public enum SDL_SYSWM_TYPE
        {
            SDL_SYSWM_UNKNOWN,
            SDL_SYSWM_WINDOWS,
            SDL_SYSWM_X11,
            SDL_SYSWM_DIRECTFB,
            SDL_SYSWM_COCOA,
            SDL_SYSWM_UIKIT,
            SDL_SYSWM_WAYLAND,
            SDL_SYSWM_MIR,
            SDL_SYSWM_WINRT,
            SDL_SYSWM_ANDROID,
            SDL_SYSWM_VIVANTE,
            SDL_SYSWM_OS2
        }

        public enum SDL_EventType : uint
        {
            SDL_FIRSTEVENT = 0,

            /* Application events */
            SDL_QUIT = 0x100,

            SDL_APP_TERMINATING,
            SDL_APP_LOWMEMORY,
            SDL_APP_WILLENTERBACKGROUND,
            SDL_APP_DIDENTERBACKGROUND,
            SDL_APP_WILLENTERFOREGROUND,
            SDL_APP_DIDENTERFOREGROUND,

            /* Window events */
            SDL_WINDOWEVENT = 0x200,
            SDL_SYSWMEVENT,

            /* Keyboard events */
            SDL_KEYDOWN = 0x300,
            SDL_KEYUP,
            SDL_TEXTEDITING,
            SDL_TEXTINPUT,
            SDL_KEYMAPCHANGED,

            /* Mouse events */
            SDL_MOUSEMOTION = 0x400,
            SDL_MOUSEBUTTONDOWN,
            SDL_MOUSEBUTTONUP,
            SDL_MOUSEWHEEL,

            /* Joystick events */
            SDL_JOYAXISMOTION = 0x600,
            SDL_JOYBALLMOTION,
            SDL_JOYHATMOTION,
            SDL_JOYBUTTONDOWN,
            SDL_JOYBUTTONUP,
            SDL_JOYDEVICEADDED,
            SDL_JOYDEVICEREMOVED,

            /* Game controller events */
            SDL_CONTROLLERAXISMOTION = 0x650,
            SDL_CONTROLLERBUTTONDOWN,
            SDL_CONTROLLERBUTTONUP,
            SDL_CONTROLLERDEVICEADDED,
            SDL_CONTROLLERDEVICEREMOVED,
            SDL_CONTROLLERDEVICEREMAPPED,

            /* Touch events */
            SDL_FINGERDOWN = 0x700,
            SDL_FINGERUP,
            SDL_FINGERMOTION,

            /* Gesture events */
            SDL_DOLLARGESTURE = 0x800,
            SDL_DOLLARRECORD,
            SDL_MULTIGESTURE,

            /* Clipboard events */
            SDL_CLIPBOARDUPDATE = 0x900,

            /* Drag and drop events */
            SDL_DROPFILE = 0x1000,
            SDL_DROPTEXT,
            SDL_DROPBEGIN,
            SDL_DROPCOMPLETE,

            /* Audio hotplug events */
            SDL_AUDIODEVICEADDED = 0x1100,
            SDL_AUDIODEVICEREMOVED,

            /* Render events */
            SDL_RENDER_TARGETS_RESET = 0x2000,
            SDL_RENDER_DEVICE_RESET,

            SDL_USEREVENT = 0x8000,

            SDL_LASTEVENT = 0xFFFF
        }

        public enum SDL_ButtonState : byte
        {
            SDL_RELEASED = 0,
            SDL_PRESSED = 1
        }

        public enum SDL_eventaction
        {
            SDL_ADDEVENT,
            SDL_PEEKEVENT,
            SDL_GETEVENT
        }

        public enum SDL_Scancode
        {
            SDL_SCANCODE_UNKNOWN = 0,

            SDL_SCANCODE_A = 4,
            SDL_SCANCODE_B = 5,
            SDL_SCANCODE_C = 6,
            SDL_SCANCODE_D = 7,
            SDL_SCANCODE_E = 8,
            SDL_SCANCODE_F = 9,
            SDL_SCANCODE_G = 10,
            SDL_SCANCODE_H = 11,
            SDL_SCANCODE_I = 12,
            SDL_SCANCODE_J = 13,
            SDL_SCANCODE_K = 14,
            SDL_SCANCODE_L = 15,
            SDL_SCANCODE_M = 16,
            SDL_SCANCODE_N = 17,
            SDL_SCANCODE_O = 18,
            SDL_SCANCODE_P = 19,
            SDL_SCANCODE_Q = 20,
            SDL_SCANCODE_R = 21,
            SDL_SCANCODE_S = 22,
            SDL_SCANCODE_T = 23,
            SDL_SCANCODE_U = 24,
            SDL_SCANCODE_V = 25,
            SDL_SCANCODE_W = 26,
            SDL_SCANCODE_X = 27,
            SDL_SCANCODE_Y = 28,
            SDL_SCANCODE_Z = 29,

            SDL_SCANCODE_1 = 30,
            SDL_SCANCODE_2 = 31,
            SDL_SCANCODE_3 = 32,
            SDL_SCANCODE_4 = 33,
            SDL_SCANCODE_5 = 34,
            SDL_SCANCODE_6 = 35,
            SDL_SCANCODE_7 = 36,
            SDL_SCANCODE_8 = 37,
            SDL_SCANCODE_9 = 38,
            SDL_SCANCODE_0 = 39,

            SDL_SCANCODE_RETURN = 40,
            SDL_SCANCODE_ESCAPE = 41,
            SDL_SCANCODE_BACKSPACE = 42,
            SDL_SCANCODE_TAB = 43,
            SDL_SCANCODE_SPACE = 44,

            SDL_SCANCODE_MINUS = 45,
            SDL_SCANCODE_EQUALS = 46,
            SDL_SCANCODE_LEFTBRACKET = 47,
            SDL_SCANCODE_RIGHTBRACKET = 48,
            SDL_SCANCODE_BACKSLASH = 49,
            SDL_SCANCODE_NONUSHASH = 50,
            SDL_SCANCODE_SEMICOLON = 51,
            SDL_SCANCODE_APOSTROPHE = 52,
            SDL_SCANCODE_GRAVE = 53,
            SDL_SCANCODE_COMMA = 54,
            SDL_SCANCODE_PERIOD = 55,
            SDL_SCANCODE_SLASH = 56,

            SDL_SCANCODE_CAPSLOCK = 57,

            SDL_SCANCODE_F1 = 58,
            SDL_SCANCODE_F2 = 59,
            SDL_SCANCODE_F3 = 60,
            SDL_SCANCODE_F4 = 61,
            SDL_SCANCODE_F5 = 62,
            SDL_SCANCODE_F6 = 63,
            SDL_SCANCODE_F7 = 64,
            SDL_SCANCODE_F8 = 65,
            SDL_SCANCODE_F9 = 66,
            SDL_SCANCODE_F10 = 67,
            SDL_SCANCODE_F11 = 68,
            SDL_SCANCODE_F12 = 69,

            SDL_SCANCODE_PRINTSCREEN = 70,
            SDL_SCANCODE_SCROLLLOCK = 71,
            SDL_SCANCODE_PAUSE = 72,
            SDL_SCANCODE_INSERT = 73,
            SDL_SCANCODE_HOME = 74,
            SDL_SCANCODE_PAGEUP = 75,
            SDL_SCANCODE_DELETE = 76,
            SDL_SCANCODE_END = 77,
            SDL_SCANCODE_PAGEDOWN = 78,
            SDL_SCANCODE_RIGHT = 79,
            SDL_SCANCODE_LEFT = 80,
            SDL_SCANCODE_DOWN = 81,
            SDL_SCANCODE_UP = 82,

            SDL_SCANCODE_NUMLOCKCLEAR = 83,
            SDL_SCANCODE_KP_DIVIDE = 84,
            SDL_SCANCODE_KP_MULTIPLY = 85,
            SDL_SCANCODE_KP_MINUS = 86,
            SDL_SCANCODE_KP_PLUS = 87,
            SDL_SCANCODE_KP_ENTER = 88,
            SDL_SCANCODE_KP_1 = 89,
            SDL_SCANCODE_KP_2 = 90,
            SDL_SCANCODE_KP_3 = 91,
            SDL_SCANCODE_KP_4 = 92,
            SDL_SCANCODE_KP_5 = 93,
            SDL_SCANCODE_KP_6 = 94,
            SDL_SCANCODE_KP_7 = 95,
            SDL_SCANCODE_KP_8 = 96,
            SDL_SCANCODE_KP_9 = 97,
            SDL_SCANCODE_KP_0 = 98,
            SDL_SCANCODE_KP_PERIOD = 99,

            SDL_SCANCODE_NONUSBACKSLASH = 100,
            SDL_SCANCODE_APPLICATION = 101,
            SDL_SCANCODE_POWER = 102,
            SDL_SCANCODE_KP_EQUALS = 103,
            SDL_SCANCODE_F13 = 104,
            SDL_SCANCODE_F14 = 105,
            SDL_SCANCODE_F15 = 106,
            SDL_SCANCODE_F16 = 107,
            SDL_SCANCODE_F17 = 108,
            SDL_SCANCODE_F18 = 109,
            SDL_SCANCODE_F19 = 110,
            SDL_SCANCODE_F20 = 111,
            SDL_SCANCODE_F21 = 112,
            SDL_SCANCODE_F22 = 113,
            SDL_SCANCODE_F23 = 114,
            SDL_SCANCODE_F24 = 115,
            SDL_SCANCODE_EXECUTE = 116,
            SDL_SCANCODE_HELP = 117,
            SDL_SCANCODE_MENU = 118,
            SDL_SCANCODE_SELECT = 119,
            SDL_SCANCODE_STOP = 120,
            SDL_SCANCODE_AGAIN = 121,
            SDL_SCANCODE_UNDO = 122,
            SDL_SCANCODE_CUT = 123,
            SDL_SCANCODE_COPY = 124,
            SDL_SCANCODE_PASTE = 125,
            SDL_SCANCODE_FIND = 126,
            SDL_SCANCODE_MUTE = 127,
            SDL_SCANCODE_VOLUMEUP = 128,
            SDL_SCANCODE_VOLUMEDOWN = 129,
            SDL_SCANCODE_KP_COMMA = 133,
            SDL_SCANCODE_KP_EQUALSAS400 = 134,

            SDL_SCANCODE_INTERNATIONAL1 = 135,
            SDL_SCANCODE_INTERNATIONAL2 = 136,
            SDL_SCANCODE_INTERNATIONAL3 = 137,
            SDL_SCANCODE_INTERNATIONAL4 = 138,
            SDL_SCANCODE_INTERNATIONAL5 = 139,
            SDL_SCANCODE_INTERNATIONAL6 = 140,
            SDL_SCANCODE_INTERNATIONAL7 = 141,
            SDL_SCANCODE_INTERNATIONAL8 = 142,
            SDL_SCANCODE_INTERNATIONAL9 = 143,
            SDL_SCANCODE_LANG1 = 144,
            SDL_SCANCODE_LANG2 = 145,
            SDL_SCANCODE_LANG3 = 146,
            SDL_SCANCODE_LANG4 = 147,
            SDL_SCANCODE_LANG5 = 148,
            SDL_SCANCODE_LANG6 = 149,
            SDL_SCANCODE_LANG7 = 150,
            SDL_SCANCODE_LANG8 = 151,
            SDL_SCANCODE_LANG9 = 152,

            SDL_SCANCODE_ALTERASE = 153,
            SDL_SCANCODE_SYSREQ = 154,
            SDL_SCANCODE_CANCEL = 155,
            SDL_SCANCODE_CLEAR = 156,
            SDL_SCANCODE_PRIOR = 157,
            SDL_SCANCODE_RETURN2 = 158,
            SDL_SCANCODE_SEPARATOR = 159,
            SDL_SCANCODE_OUT = 160,
            SDL_SCANCODE_OPER = 161,
            SDL_SCANCODE_CLEARAGAIN = 162,
            SDL_SCANCODE_CRSEL = 163,
            SDL_SCANCODE_EXSEL = 164,

            SDL_SCANCODE_KP_00 = 176,
            SDL_SCANCODE_KP_000 = 177,
            SDL_SCANCODE_THOUSANDSSEPARATOR = 178,
            SDL_SCANCODE_DECIMALSEPARATOR = 179,
            SDL_SCANCODE_CURRENCYUNIT = 180,
            SDL_SCANCODE_CURRENCYSUBUNIT = 181,
            SDL_SCANCODE_KP_LEFTPAREN = 182,
            SDL_SCANCODE_KP_RIGHTPAREN = 183,
            SDL_SCANCODE_KP_LEFTBRACE = 184,
            SDL_SCANCODE_KP_RIGHTBRACE = 185,
            SDL_SCANCODE_KP_TAB = 186,
            SDL_SCANCODE_KP_BACKSPACE = 187,
            SDL_SCANCODE_KP_A = 188,
            SDL_SCANCODE_KP_B = 189,
            SDL_SCANCODE_KP_C = 190,
            SDL_SCANCODE_KP_D = 191,
            SDL_SCANCODE_KP_E = 192,
            SDL_SCANCODE_KP_F = 193,
            SDL_SCANCODE_KP_XOR = 194,
            SDL_SCANCODE_KP_POWER = 195,
            SDL_SCANCODE_KP_PERCENT = 196,
            SDL_SCANCODE_KP_LESS = 197,
            SDL_SCANCODE_KP_GREATER = 198,
            SDL_SCANCODE_KP_AMPERSAND = 199,
            SDL_SCANCODE_KP_DBLAMPERSAND = 200,
            SDL_SCANCODE_KP_VERTICALBAR = 201,
            SDL_SCANCODE_KP_DBLVERTICALBAR = 202,
            SDL_SCANCODE_KP_COLON = 203,
            SDL_SCANCODE_KP_HASH = 204,
            SDL_SCANCODE_KP_SPACE = 205,
            SDL_SCANCODE_KP_AT = 206,
            SDL_SCANCODE_KP_EXCLAM = 207,
            SDL_SCANCODE_KP_MEMSTORE = 208,
            SDL_SCANCODE_KP_MEMRECALL = 209,
            SDL_SCANCODE_KP_MEMCLEAR = 210,
            SDL_SCANCODE_KP_MEMADD = 211,
            SDL_SCANCODE_KP_MEMSUBTRACT = 212,
            SDL_SCANCODE_KP_MEMMULTIPLY = 213,
            SDL_SCANCODE_KP_MEMDIVIDE = 214,
            SDL_SCANCODE_KP_PLUSMINUS = 215,
            SDL_SCANCODE_KP_CLEAR = 216,
            SDL_SCANCODE_KP_CLEARENTRY = 217,
            SDL_SCANCODE_KP_BINARY = 218,
            SDL_SCANCODE_KP_OCTAL = 219,
            SDL_SCANCODE_KP_DECIMAL = 220,
            SDL_SCANCODE_KP_HEXADECIMAL = 221,

            SDL_SCANCODE_LCTRL = 224,
            SDL_SCANCODE_LSHIFT = 225,
            SDL_SCANCODE_LALT = 226,
            SDL_SCANCODE_LGUI = 227,
            SDL_SCANCODE_RCTRL = 228,
            SDL_SCANCODE_RSHIFT = 229,
            SDL_SCANCODE_RALT = 230,
            SDL_SCANCODE_RGUI = 231,

            SDL_SCANCODE_MODE = 257,

            SDL_SCANCODE_AUDIONEXT = 258,
            SDL_SCANCODE_AUDIOPREV = 259,
            SDL_SCANCODE_AUDIOSTOP = 260,
            SDL_SCANCODE_AUDIOPLAY = 261,
            SDL_SCANCODE_AUDIOMUTE = 262,
            SDL_SCANCODE_MEDIASELECT = 263,
            SDL_SCANCODE_WWW = 264,
            SDL_SCANCODE_MAIL = 265,
            SDL_SCANCODE_CALCULATOR = 266,
            SDL_SCANCODE_COMPUTER = 267,
            SDL_SCANCODE_AC_SEARCH = 268,
            SDL_SCANCODE_AC_HOME = 269,
            SDL_SCANCODE_AC_BACK = 270,
            SDL_SCANCODE_AC_FORWARD = 271,
            SDL_SCANCODE_AC_STOP = 272,
            SDL_SCANCODE_AC_REFRESH = 273,
            SDL_SCANCODE_AC_BOOKMARKS = 274,

            SDL_SCANCODE_BRIGHTNESSDOWN = 275,
            SDL_SCANCODE_BRIGHTNESSUP = 276,
            SDL_SCANCODE_DISPLAYSWITCH = 277,
            SDL_SCANCODE_KBDILLUMTOGGLE = 278,
            SDL_SCANCODE_KBDILLUMDOWN = 279,
            SDL_SCANCODE_KBDILLUMUP = 280,
            SDL_SCANCODE_EJECT = 281,
            SDL_SCANCODE_SLEEP = 282,

            SDL_SCANCODE_APP1 = 283,
            SDL_SCANCODE_APP2 = 284,

            SDL_SCANCODE_AUDIOREWIND = 285,
            SDL_SCANCODE_AUDIOFASTFORWARD = 286,

            SDL_NUM_SCANCODES = 512
        }

        public enum SDL_Keycode
        {
            SDLK_UNKNOWN = 0,

            SDLK_RETURN = '\r',
            SDLK_ESCAPE = 27,
            SDLK_BACKSPACE = '\b',
            SDLK_TAB = '\t',
            SDLK_SPACE = ' ',
            SDLK_EXCLAIM = '!',
            SDLK_QUOTEDBL = '"',
            SDLK_HASH = '#',
            SDLK_PERCENT = '%',
            SDLK_DOLLAR = '$',
            SDLK_AMPERSAND = '&',
            SDLK_QUOTE = '\'',
            SDLK_LEFTPAREN = '(',
            SDLK_RIGHTPAREN = ')',
            SDLK_ASTERISK = '*',
            SDLK_PLUS = '+',
            SDLK_COMMA = ',',
            SDLK_MINUS = '-',
            SDLK_PERIOD = '.',
            SDLK_SLASH = '/',
            SDLK_0 = '0',
            SDLK_1 = '1',
            SDLK_2 = '2',
            SDLK_3 = '3',
            SDLK_4 = '4',
            SDLK_5 = '5',
            SDLK_6 = '6',
            SDLK_7 = '7',
            SDLK_8 = '8',
            SDLK_9 = '9',
            SDLK_COLON = ':',
            SDLK_SEMICOLON = ';',
            SDLK_LESS = '<',
            SDLK_EQUALS = '=',
            SDLK_GREATER = '>',
            SDLK_QUESTION = '?',
            SDLK_AT = '@',

            /*
               Skip uppercase letters
             */
            SDLK_LEFTBRACKET = '[',
            SDLK_BACKSLASH = '\\',
            SDLK_RIGHTBRACKET = ']',
            SDLK_CARET = '^',
            SDLK_UNDERSCORE = '_',
            SDLK_BACKQUOTE = '`',
            SDLK_a = 'a',
            SDLK_b = 'b',
            SDLK_c = 'c',
            SDLK_d = 'd',
            SDLK_e = 'e',
            SDLK_f = 'f',
            SDLK_g = 'g',
            SDLK_h = 'h',
            SDLK_i = 'i',
            SDLK_j = 'j',
            SDLK_k = 'k',
            SDLK_l = 'l',
            SDLK_m = 'm',
            SDLK_n = 'n',
            SDLK_o = 'o',
            SDLK_p = 'p',
            SDLK_q = 'q',
            SDLK_r = 'r',
            SDLK_s = 's',
            SDLK_t = 't',
            SDLK_u = 'u',
            SDLK_v = 'v',
            SDLK_w = 'w',
            SDLK_x = 'x',
            SDLK_y = 'y',
            SDLK_z = 'z',

            SDLK_CAPSLOCK = SDL_Scancode.SDL_SCANCODE_CAPSLOCK | SDLK_SCANCODE_MASK,

            SDLK_F1 = SDL_Scancode.SDL_SCANCODE_F1 | SDLK_SCANCODE_MASK,
            SDLK_F2 = SDL_Scancode.SDL_SCANCODE_F2 | SDLK_SCANCODE_MASK,
            SDLK_F3 = SDL_Scancode.SDL_SCANCODE_F3 | SDLK_SCANCODE_MASK,
            SDLK_F4 = SDL_Scancode.SDL_SCANCODE_F4 | SDLK_SCANCODE_MASK,
            SDLK_F5 = SDL_Scancode.SDL_SCANCODE_F5 | SDLK_SCANCODE_MASK,
            SDLK_F6 = SDL_Scancode.SDL_SCANCODE_F6 | SDLK_SCANCODE_MASK,
            SDLK_F7 = SDL_Scancode.SDL_SCANCODE_F7 | SDLK_SCANCODE_MASK,
            SDLK_F8 = SDL_Scancode.SDL_SCANCODE_F8 | SDLK_SCANCODE_MASK,
            SDLK_F9 = SDL_Scancode.SDL_SCANCODE_F9 | SDLK_SCANCODE_MASK,
            SDLK_F10 = SDL_Scancode.SDL_SCANCODE_F10 | SDLK_SCANCODE_MASK,
            SDLK_F11 = SDL_Scancode.SDL_SCANCODE_F11 | SDLK_SCANCODE_MASK,
            SDLK_F12 = SDL_Scancode.SDL_SCANCODE_F12 | SDLK_SCANCODE_MASK,

            SDLK_PRINTSCREEN = SDL_Scancode.SDL_SCANCODE_PRINTSCREEN | SDLK_SCANCODE_MASK,
            SDLK_SCROLLLOCK = SDL_Scancode.SDL_SCANCODE_SCROLLLOCK | SDLK_SCANCODE_MASK,
            SDLK_PAUSE = SDL_Scancode.SDL_SCANCODE_PAUSE | SDLK_SCANCODE_MASK,
            SDLK_INSERT = SDL_Scancode.SDL_SCANCODE_INSERT | SDLK_SCANCODE_MASK,
            SDLK_HOME = SDL_Scancode.SDL_SCANCODE_HOME | SDLK_SCANCODE_MASK,
            SDLK_PAGEUP = SDL_Scancode.SDL_SCANCODE_PAGEUP | SDLK_SCANCODE_MASK,
            SDLK_DELETE = 127,
            SDLK_END = SDL_Scancode.SDL_SCANCODE_END | SDLK_SCANCODE_MASK,
            SDLK_PAGEDOWN = SDL_Scancode.SDL_SCANCODE_PAGEDOWN | SDLK_SCANCODE_MASK,
            SDLK_RIGHT = SDL_Scancode.SDL_SCANCODE_RIGHT | SDLK_SCANCODE_MASK,
            SDLK_LEFT = SDL_Scancode.SDL_SCANCODE_LEFT | SDLK_SCANCODE_MASK,
            SDLK_DOWN = SDL_Scancode.SDL_SCANCODE_DOWN | SDLK_SCANCODE_MASK,
            SDLK_UP = SDL_Scancode.SDL_SCANCODE_UP | SDLK_SCANCODE_MASK,

            SDLK_NUMLOCKCLEAR = SDL_Scancode.SDL_SCANCODE_NUMLOCKCLEAR | SDLK_SCANCODE_MASK,
            SDLK_KP_DIVIDE = SDL_Scancode.SDL_SCANCODE_KP_DIVIDE | SDLK_SCANCODE_MASK,
            SDLK_KP_MULTIPLY = SDL_Scancode.SDL_SCANCODE_KP_MULTIPLY | SDLK_SCANCODE_MASK,
            SDLK_KP_MINUS = SDL_Scancode.SDL_SCANCODE_KP_MINUS | SDLK_SCANCODE_MASK,
            SDLK_KP_PLUS = SDL_Scancode.SDL_SCANCODE_KP_PLUS | SDLK_SCANCODE_MASK,
            SDLK_KP_ENTER = SDL_Scancode.SDL_SCANCODE_KP_ENTER | SDLK_SCANCODE_MASK,
            SDLK_KP_1 = SDL_Scancode.SDL_SCANCODE_KP_1 | SDLK_SCANCODE_MASK,
            SDLK_KP_2 = SDL_Scancode.SDL_SCANCODE_KP_2 | SDLK_SCANCODE_MASK,
            SDLK_KP_3 = SDL_Scancode.SDL_SCANCODE_KP_3 | SDLK_SCANCODE_MASK,
            SDLK_KP_4 = SDL_Scancode.SDL_SCANCODE_KP_4 | SDLK_SCANCODE_MASK,
            SDLK_KP_5 = SDL_Scancode.SDL_SCANCODE_KP_5 | SDLK_SCANCODE_MASK,
            SDLK_KP_6 = SDL_Scancode.SDL_SCANCODE_KP_6 | SDLK_SCANCODE_MASK,
            SDLK_KP_7 = SDL_Scancode.SDL_SCANCODE_KP_7 | SDLK_SCANCODE_MASK,
            SDLK_KP_8 = SDL_Scancode.SDL_SCANCODE_KP_8 | SDLK_SCANCODE_MASK,
            SDLK_KP_9 = SDL_Scancode.SDL_SCANCODE_KP_9 | SDLK_SCANCODE_MASK,
            SDLK_KP_0 = SDL_Scancode.SDL_SCANCODE_KP_0 | SDLK_SCANCODE_MASK,
            SDLK_KP_PERIOD = SDL_Scancode.SDL_SCANCODE_KP_PERIOD | SDLK_SCANCODE_MASK,

            SDLK_APPLICATION = SDL_Scancode.SDL_SCANCODE_APPLICATION | SDLK_SCANCODE_MASK,
            SDLK_POWER = SDL_Scancode.SDL_SCANCODE_POWER | SDLK_SCANCODE_MASK,
            SDLK_KP_EQUALS = SDL_Scancode.SDL_SCANCODE_KP_EQUALS | SDLK_SCANCODE_MASK,
            SDLK_F13 = SDL_Scancode.SDL_SCANCODE_F13 | SDLK_SCANCODE_MASK,
            SDLK_F14 = SDL_Scancode.SDL_SCANCODE_F14 | SDLK_SCANCODE_MASK,
            SDLK_F15 = SDL_Scancode.SDL_SCANCODE_F15 | SDLK_SCANCODE_MASK,
            SDLK_F16 = SDL_Scancode.SDL_SCANCODE_F16 | SDLK_SCANCODE_MASK,
            SDLK_F17 = SDL_Scancode.SDL_SCANCODE_F17 | SDLK_SCANCODE_MASK,
            SDLK_F18 = SDL_Scancode.SDL_SCANCODE_F18 | SDLK_SCANCODE_MASK,
            SDLK_F19 = SDL_Scancode.SDL_SCANCODE_F19 | SDLK_SCANCODE_MASK,
            SDLK_F20 = SDL_Scancode.SDL_SCANCODE_F20 | SDLK_SCANCODE_MASK,
            SDLK_F21 = SDL_Scancode.SDL_SCANCODE_F21 | SDLK_SCANCODE_MASK,
            SDLK_F22 = SDL_Scancode.SDL_SCANCODE_F22 | SDLK_SCANCODE_MASK,
            SDLK_F23 = SDL_Scancode.SDL_SCANCODE_F23 | SDLK_SCANCODE_MASK,
            SDLK_F24 = SDL_Scancode.SDL_SCANCODE_F24 | SDLK_SCANCODE_MASK,
            SDLK_EXECUTE = SDL_Scancode.SDL_SCANCODE_EXECUTE | SDLK_SCANCODE_MASK,
            SDLK_HELP = SDL_Scancode.SDL_SCANCODE_HELP | SDLK_SCANCODE_MASK,
            SDLK_MENU = SDL_Scancode.SDL_SCANCODE_MENU | SDLK_SCANCODE_MASK,
            SDLK_SELECT = SDL_Scancode.SDL_SCANCODE_SELECT | SDLK_SCANCODE_MASK,
            SDLK_STOP = SDL_Scancode.SDL_SCANCODE_STOP | SDLK_SCANCODE_MASK,
            SDLK_AGAIN = SDL_Scancode.SDL_SCANCODE_AGAIN | SDLK_SCANCODE_MASK,
            SDLK_UNDO = SDL_Scancode.SDL_SCANCODE_UNDO | SDLK_SCANCODE_MASK,
            SDLK_CUT = SDL_Scancode.SDL_SCANCODE_CUT | SDLK_SCANCODE_MASK,
            SDLK_COPY = SDL_Scancode.SDL_SCANCODE_COPY | SDLK_SCANCODE_MASK,
            SDLK_PASTE = SDL_Scancode.SDL_SCANCODE_PASTE | SDLK_SCANCODE_MASK,
            SDLK_FIND = SDL_Scancode.SDL_SCANCODE_FIND | SDLK_SCANCODE_MASK,
            SDLK_MUTE = SDL_Scancode.SDL_SCANCODE_MUTE | SDLK_SCANCODE_MASK,
            SDLK_VOLUMEUP = SDL_Scancode.SDL_SCANCODE_VOLUMEUP | SDLK_SCANCODE_MASK,
            SDLK_VOLUMEDOWN = SDL_Scancode.SDL_SCANCODE_VOLUMEDOWN | SDLK_SCANCODE_MASK,
            SDLK_KP_COMMA = SDL_Scancode.SDL_SCANCODE_KP_COMMA | SDLK_SCANCODE_MASK,

            SDLK_KP_EQUALSAS400 =
                SDL_Scancode.SDL_SCANCODE_KP_EQUALSAS400 | SDLK_SCANCODE_MASK,

            SDLK_ALTERASE = SDL_Scancode.SDL_SCANCODE_ALTERASE | SDLK_SCANCODE_MASK,
            SDLK_SYSREQ = SDL_Scancode.SDL_SCANCODE_SYSREQ | SDLK_SCANCODE_MASK,
            SDLK_CANCEL = SDL_Scancode.SDL_SCANCODE_CANCEL | SDLK_SCANCODE_MASK,
            SDLK_CLEAR = SDL_Scancode.SDL_SCANCODE_CLEAR | SDLK_SCANCODE_MASK,
            SDLK_PRIOR = SDL_Scancode.SDL_SCANCODE_PRIOR | SDLK_SCANCODE_MASK,
            SDLK_RETURN2 = SDL_Scancode.SDL_SCANCODE_RETURN2 | SDLK_SCANCODE_MASK,
            SDLK_SEPARATOR = SDL_Scancode.SDL_SCANCODE_SEPARATOR | SDLK_SCANCODE_MASK,
            SDLK_OUT = SDL_Scancode.SDL_SCANCODE_OUT | SDLK_SCANCODE_MASK,
            SDLK_OPER = SDL_Scancode.SDL_SCANCODE_OPER | SDLK_SCANCODE_MASK,
            SDLK_CLEARAGAIN = SDL_Scancode.SDL_SCANCODE_CLEARAGAIN | SDLK_SCANCODE_MASK,
            SDLK_CRSEL = SDL_Scancode.SDL_SCANCODE_CRSEL | SDLK_SCANCODE_MASK,
            SDLK_EXSEL = SDL_Scancode.SDL_SCANCODE_EXSEL | SDLK_SCANCODE_MASK,

            SDLK_KP_00 = SDL_Scancode.SDL_SCANCODE_KP_00 | SDLK_SCANCODE_MASK,
            SDLK_KP_000 = SDL_Scancode.SDL_SCANCODE_KP_000 | SDLK_SCANCODE_MASK,

            SDLK_THOUSANDSSEPARATOR =
                SDL_Scancode.SDL_SCANCODE_THOUSANDSSEPARATOR | SDLK_SCANCODE_MASK,

            SDLK_DECIMALSEPARATOR =
                SDL_Scancode.SDL_SCANCODE_DECIMALSEPARATOR | SDLK_SCANCODE_MASK,
            SDLK_CURRENCYUNIT = SDL_Scancode.SDL_SCANCODE_CURRENCYUNIT | SDLK_SCANCODE_MASK,

            SDLK_CURRENCYSUBUNIT =
                SDL_Scancode.SDL_SCANCODE_CURRENCYSUBUNIT | SDLK_SCANCODE_MASK,
            SDLK_KP_LEFTPAREN = SDL_Scancode.SDL_SCANCODE_KP_LEFTPAREN | SDLK_SCANCODE_MASK,
            SDLK_KP_RIGHTPAREN = SDL_Scancode.SDL_SCANCODE_KP_RIGHTPAREN | SDLK_SCANCODE_MASK,
            SDLK_KP_LEFTBRACE = SDL_Scancode.SDL_SCANCODE_KP_LEFTBRACE | SDLK_SCANCODE_MASK,
            SDLK_KP_RIGHTBRACE = SDL_Scancode.SDL_SCANCODE_KP_RIGHTBRACE | SDLK_SCANCODE_MASK,
            SDLK_KP_TAB = SDL_Scancode.SDL_SCANCODE_KP_TAB | SDLK_SCANCODE_MASK,
            SDLK_KP_BACKSPACE = SDL_Scancode.SDL_SCANCODE_KP_BACKSPACE | SDLK_SCANCODE_MASK,
            SDLK_KP_A = SDL_Scancode.SDL_SCANCODE_KP_A | SDLK_SCANCODE_MASK,
            SDLK_KP_B = SDL_Scancode.SDL_SCANCODE_KP_B | SDLK_SCANCODE_MASK,
            SDLK_KP_C = SDL_Scancode.SDL_SCANCODE_KP_C | SDLK_SCANCODE_MASK,
            SDLK_KP_D = SDL_Scancode.SDL_SCANCODE_KP_D | SDLK_SCANCODE_MASK,
            SDLK_KP_E = SDL_Scancode.SDL_SCANCODE_KP_E | SDLK_SCANCODE_MASK,
            SDLK_KP_F = SDL_Scancode.SDL_SCANCODE_KP_F | SDLK_SCANCODE_MASK,
            SDLK_KP_XOR = SDL_Scancode.SDL_SCANCODE_KP_XOR | SDLK_SCANCODE_MASK,
            SDLK_KP_POWER = SDL_Scancode.SDL_SCANCODE_KP_POWER | SDLK_SCANCODE_MASK,
            SDLK_KP_PERCENT = SDL_Scancode.SDL_SCANCODE_KP_PERCENT | SDLK_SCANCODE_MASK,
            SDLK_KP_LESS = SDL_Scancode.SDL_SCANCODE_KP_LESS | SDLK_SCANCODE_MASK,
            SDLK_KP_GREATER = SDL_Scancode.SDL_SCANCODE_KP_GREATER | SDLK_SCANCODE_MASK,
            SDLK_KP_AMPERSAND = SDL_Scancode.SDL_SCANCODE_KP_AMPERSAND | SDLK_SCANCODE_MASK,

            SDLK_KP_DBLAMPERSAND =
                SDL_Scancode.SDL_SCANCODE_KP_DBLAMPERSAND | SDLK_SCANCODE_MASK,

            SDLK_KP_VERTICALBAR =
                SDL_Scancode.SDL_SCANCODE_KP_VERTICALBAR | SDLK_SCANCODE_MASK,

            SDLK_KP_DBLVERTICALBAR =
                SDL_Scancode.SDL_SCANCODE_KP_DBLVERTICALBAR | SDLK_SCANCODE_MASK,
            SDLK_KP_COLON = SDL_Scancode.SDL_SCANCODE_KP_COLON | SDLK_SCANCODE_MASK,
            SDLK_KP_HASH = SDL_Scancode.SDL_SCANCODE_KP_HASH | SDLK_SCANCODE_MASK,
            SDLK_KP_SPACE = SDL_Scancode.SDL_SCANCODE_KP_SPACE | SDLK_SCANCODE_MASK,
            SDLK_KP_AT = SDL_Scancode.SDL_SCANCODE_KP_AT | SDLK_SCANCODE_MASK,
            SDLK_KP_EXCLAM = SDL_Scancode.SDL_SCANCODE_KP_EXCLAM | SDLK_SCANCODE_MASK,
            SDLK_KP_MEMSTORE = SDL_Scancode.SDL_SCANCODE_KP_MEMSTORE | SDLK_SCANCODE_MASK,
            SDLK_KP_MEMRECALL = SDL_Scancode.SDL_SCANCODE_KP_MEMRECALL | SDLK_SCANCODE_MASK,
            SDLK_KP_MEMCLEAR = SDL_Scancode.SDL_SCANCODE_KP_MEMCLEAR | SDLK_SCANCODE_MASK,
            SDLK_KP_MEMADD = SDL_Scancode.SDL_SCANCODE_KP_MEMADD | SDLK_SCANCODE_MASK,

            SDLK_KP_MEMSUBTRACT =
                SDL_Scancode.SDL_SCANCODE_KP_MEMSUBTRACT | SDLK_SCANCODE_MASK,

            SDLK_KP_MEMMULTIPLY =
                SDL_Scancode.SDL_SCANCODE_KP_MEMMULTIPLY | SDLK_SCANCODE_MASK,
            SDLK_KP_MEMDIVIDE = SDL_Scancode.SDL_SCANCODE_KP_MEMDIVIDE | SDLK_SCANCODE_MASK,
            SDLK_KP_PLUSMINUS = SDL_Scancode.SDL_SCANCODE_KP_PLUSMINUS | SDLK_SCANCODE_MASK,
            SDLK_KP_CLEAR = SDL_Scancode.SDL_SCANCODE_KP_CLEAR | SDLK_SCANCODE_MASK,
            SDLK_KP_CLEARENTRY = SDL_Scancode.SDL_SCANCODE_KP_CLEARENTRY | SDLK_SCANCODE_MASK,
            SDLK_KP_BINARY = SDL_Scancode.SDL_SCANCODE_KP_BINARY | SDLK_SCANCODE_MASK,
            SDLK_KP_OCTAL = SDL_Scancode.SDL_SCANCODE_KP_OCTAL | SDLK_SCANCODE_MASK,
            SDLK_KP_DECIMAL = SDL_Scancode.SDL_SCANCODE_KP_DECIMAL | SDLK_SCANCODE_MASK,

            SDLK_KP_HEXADECIMAL =
                SDL_Scancode.SDL_SCANCODE_KP_HEXADECIMAL | SDLK_SCANCODE_MASK,

            SDLK_LCTRL = SDL_Scancode.SDL_SCANCODE_LCTRL | SDLK_SCANCODE_MASK,
            SDLK_LSHIFT = SDL_Scancode.SDL_SCANCODE_LSHIFT | SDLK_SCANCODE_MASK,
            SDLK_LALT = SDL_Scancode.SDL_SCANCODE_LALT | SDLK_SCANCODE_MASK,
            SDLK_LGUI = SDL_Scancode.SDL_SCANCODE_LGUI | SDLK_SCANCODE_MASK,
            SDLK_RCTRL = SDL_Scancode.SDL_SCANCODE_RCTRL | SDLK_SCANCODE_MASK,
            SDLK_RSHIFT = SDL_Scancode.SDL_SCANCODE_RSHIFT | SDLK_SCANCODE_MASK,
            SDLK_RALT = SDL_Scancode.SDL_SCANCODE_RALT | SDLK_SCANCODE_MASK,
            SDLK_RGUI = SDL_Scancode.SDL_SCANCODE_RGUI | SDLK_SCANCODE_MASK,

            SDLK_MODE = SDL_Scancode.SDL_SCANCODE_MODE | SDLK_SCANCODE_MASK,

            SDLK_AUDIONEXT = SDL_Scancode.SDL_SCANCODE_AUDIONEXT | SDLK_SCANCODE_MASK,
            SDLK_AUDIOPREV = SDL_Scancode.SDL_SCANCODE_AUDIOPREV | SDLK_SCANCODE_MASK,
            SDLK_AUDIOSTOP = SDL_Scancode.SDL_SCANCODE_AUDIOSTOP | SDLK_SCANCODE_MASK,
            SDLK_AUDIOPLAY = SDL_Scancode.SDL_SCANCODE_AUDIOPLAY | SDLK_SCANCODE_MASK,
            SDLK_AUDIOMUTE = SDL_Scancode.SDL_SCANCODE_AUDIOMUTE | SDLK_SCANCODE_MASK,
            SDLK_MEDIASELECT = SDL_Scancode.SDL_SCANCODE_MEDIASELECT | SDLK_SCANCODE_MASK,
            SDLK_WWW = SDL_Scancode.SDL_SCANCODE_WWW | SDLK_SCANCODE_MASK,
            SDLK_MAIL = SDL_Scancode.SDL_SCANCODE_MAIL | SDLK_SCANCODE_MASK,
            SDLK_CALCULATOR = SDL_Scancode.SDL_SCANCODE_CALCULATOR | SDLK_SCANCODE_MASK,
            SDLK_COMPUTER = SDL_Scancode.SDL_SCANCODE_COMPUTER | SDLK_SCANCODE_MASK,
            SDLK_AC_SEARCH = SDL_Scancode.SDL_SCANCODE_AC_SEARCH | SDLK_SCANCODE_MASK,
            SDLK_AC_HOME = SDL_Scancode.SDL_SCANCODE_AC_HOME | SDLK_SCANCODE_MASK,
            SDLK_AC_BACK = SDL_Scancode.SDL_SCANCODE_AC_BACK | SDLK_SCANCODE_MASK,
            SDLK_AC_FORWARD = SDL_Scancode.SDL_SCANCODE_AC_FORWARD | SDLK_SCANCODE_MASK,
            SDLK_AC_STOP = SDL_Scancode.SDL_SCANCODE_AC_STOP | SDLK_SCANCODE_MASK,
            SDLK_AC_REFRESH = SDL_Scancode.SDL_SCANCODE_AC_REFRESH | SDLK_SCANCODE_MASK,
            SDLK_AC_BOOKMARKS = SDL_Scancode.SDL_SCANCODE_AC_BOOKMARKS | SDLK_SCANCODE_MASK,

            SDLK_BRIGHTNESSDOWN =
                SDL_Scancode.SDL_SCANCODE_BRIGHTNESSDOWN | SDLK_SCANCODE_MASK,
            SDLK_BRIGHTNESSUP = SDL_Scancode.SDL_SCANCODE_BRIGHTNESSUP | SDLK_SCANCODE_MASK,
            SDLK_DISPLAYSWITCH = SDL_Scancode.SDL_SCANCODE_DISPLAYSWITCH | SDLK_SCANCODE_MASK,

            SDLK_KBDILLUMTOGGLE =
                SDL_Scancode.SDL_SCANCODE_KBDILLUMTOGGLE | SDLK_SCANCODE_MASK,
            SDLK_KBDILLUMDOWN = SDL_Scancode.SDL_SCANCODE_KBDILLUMDOWN | SDLK_SCANCODE_MASK,
            SDLK_KBDILLUMUP = SDL_Scancode.SDL_SCANCODE_KBDILLUMUP | SDLK_SCANCODE_MASK,
            SDLK_EJECT = SDL_Scancode.SDL_SCANCODE_EJECT | SDLK_SCANCODE_MASK,
            SDLK_SLEEP = SDL_Scancode.SDL_SCANCODE_SLEEP | SDLK_SCANCODE_MASK,
            SDLK_APP1 = SDL_Scancode.SDL_SCANCODE_APP1 | SDLK_SCANCODE_MASK,
            SDLK_APP2 = SDL_Scancode.SDL_SCANCODE_APP2 | SDLK_SCANCODE_MASK,

            SDLK_AUDIOREWIND = SDL_Scancode.SDL_SCANCODE_AUDIOREWIND | SDLK_SCANCODE_MASK,
            SDLK_AUDIOFASTFORWARD = SDL_Scancode.SDL_SCANCODE_AUDIOFASTFORWARD | SDLK_SCANCODE_MASK
        }

        [Flags]
        public enum SDL_Keymod : ushort
        {
            KMOD_NONE = 0x0000,
            KMOD_LSHIFT = 0x0001,
            KMOD_RSHIFT = 0x0002,
            KMOD_LCTRL = 0x0040,
            KMOD_RCTRL = 0x0080,
            KMOD_LALT = 0x0100,
            KMOD_RALT = 0x0200,
            KMOD_LGUI = 0x0400,
            KMOD_RGUI = 0x0800,
            KMOD_NUM = 0x1000,
            KMOD_CAPS = 0x2000,
            KMOD_MODE = 0x4000,
            KMOD_RESERVED = 0x8000,

            KMOD_CTRL = KMOD_LCTRL | KMOD_RCTRL,
            KMOD_SHIFT = KMOD_LSHIFT | KMOD_RSHIFT,
            KMOD_ALT = KMOD_LALT | KMOD_RALT,
            KMOD_GUI = KMOD_LGUI | KMOD_RGUI
        }

        public enum SDL_SystemCursor
        {
            SDL_SYSTEM_CURSOR_ARROW,
            SDL_SYSTEM_CURSOR_IBEAM,
            SDL_SYSTEM_CURSOR_WAIT,
            SDL_SYSTEM_CURSOR_CROSSHAIR,
            SDL_SYSTEM_CURSOR_WAITARROW,
            SDL_SYSTEM_CURSOR_SIZENWSE,
            SDL_SYSTEM_CURSOR_SIZENESW,
            SDL_SYSTEM_CURSOR_SIZEWE,
            SDL_SYSTEM_CURSOR_SIZENS,
            SDL_SYSTEM_CURSOR_SIZEALL,
            SDL_SYSTEM_CURSOR_NO,
            SDL_SYSTEM_CURSOR_HAND,
            SDL_NUM_SYSTEM_CURSORS
        }

        public enum SDL_MouseWheelDirection : uint
        {
            SDL_MOUSEWHEEL_NORMAL,
            SDL_MOUSEWHEEL_FLIPPED
        }

        public enum SDL_Button : uint
        {
            SDL_BUTTON_LEFT = 1,
            SDL_BUTTON_MIDDLE = 2,
            SDL_BUTTON_RIGHT = 3,
            SDL_BUTTON_X1 = 4,
            SDL_BUTTON_X2 = 5,
        }

        [Flags]
        public enum SDL_ButtonMask : uint
        {
            SDL_BUTTON_LMASK = 1 << ((int) SDL_Button.SDL_BUTTON_LEFT - 1),
            SDL_BUTTON_MMASK = 1 << ((int) SDL_Button.SDL_BUTTON_MIDDLE - 1),
            SDL_BUTTON_RMASK = 1 << ((int) SDL_Button.SDL_BUTTON_RIGHT - 1),
            SDL_BUTTON_X1MASK = 1 << ((int) SDL_Button.SDL_BUTTON_X1 - 1),
            SDL_BUTTON_X2MASK = 1 << ((int) SDL_Button.SDL_BUTTON_X2 - 1)
        }

        public enum SDL_JoystickType
        {
            SDL_JOYSTICK_TYPE_UNKNOWN,
            SDL_JOYSTICK_TYPE_GAMECONTROLLER,
            SDL_JOYSTICK_TYPE_WHEEL,
            SDL_JOYSTICK_TYPE_ARCADE_STICK,
            SDL_JOYSTICK_TYPE_FLIGHT_STICK,
            SDL_JOYSTICK_TYPE_DANCE_PAD,
            SDL_JOYSTICK_TYPE_GUITAR,
            SDL_JOYSTICK_TYPE_DRUM_KIT,
            SDL_JOYSTICK_TYPE_ARCADE_PAD,
            SDL_JOYSTICK_TYPE_THROTTLE
        }

        public enum SDL_JoystickPowerLevel
        {
            SDL_JOYSTICK_POWER_UNKNOWN = -1,
            SDL_JOYSTICK_POWER_EMPTY,
            SDL_JOYSTICK_POWER_LOW,
            SDL_JOYSTICK_POWER_MEDIUM,
            SDL_JOYSTICK_POWER_FULL,
            SDL_JOYSTICK_POWER_WIRED,
            SDL_JOYSTICK_POWER_MAX
        }

        public enum SDL_Hat : byte
        {
            SDL_HAT_CENTERED = 0x00,
            SDL_HAT_UP = 0x01,
            SDL_HAT_RIGHT = 0x02,
            SDL_HAT_DOWN = 0x04,
            SDL_HAT_LEFT = 0x08,

            SDL_HAT_RIGHTUP = SDL_HAT_RIGHT | SDL_HAT_UP,
            SDL_HAT_RIGHTDOWN = SDL_HAT_RIGHT | SDL_HAT_DOWN,
            SDL_HAT_LEFTUP = SDL_HAT_LEFT | SDL_HAT_UP,
            SDL_HAT_LEFTDOWN = SDL_HAT_LEFT | SDL_HAT_DOWN
        }

        public enum SDL_GameControllerBindType
        {
            SDL_CONTROLLER_BINDTYPE_NONE = 0,
            SDL_CONTROLLER_BINDTYPE_BUTTON,
            SDL_CONTROLLER_BINDTYPE_AXIS,
            SDL_CONTROLLER_BINDTYPE_HAT
        }

        public enum SDL_GameControllerAxis
        {
            SDL_CONTROLLER_AXIS_INVALID = -1,
            SDL_CONTROLLER_AXIS_LEFTX,
            SDL_CONTROLLER_AXIS_LEFTY,
            SDL_CONTROLLER_AXIS_RIGHTX,
            SDL_CONTROLLER_AXIS_RIGHTY,
            SDL_CONTROLLER_AXIS_TRIGGERLEFT,
            SDL_CONTROLLER_AXIS_TRIGGERRIGHT,
            SDL_CONTROLLER_AXIS_MAX
        }

        public enum SDL_GameControllerButton
        {
            SDL_CONTROLLER_BUTTON_INVALID = -1,
            SDL_CONTROLLER_BUTTON_A,
            SDL_CONTROLLER_BUTTON_B,
            SDL_CONTROLLER_BUTTON_X,
            SDL_CONTROLLER_BUTTON_Y,
            SDL_CONTROLLER_BUTTON_BACK,
            SDL_CONTROLLER_BUTTON_GUIDE,
            SDL_CONTROLLER_BUTTON_START,
            SDL_CONTROLLER_BUTTON_LEFTSTICK,
            SDL_CONTROLLER_BUTTON_RIGHTSTICK,
            SDL_CONTROLLER_BUTTON_LEFTSHOULDER,
            SDL_CONTROLLER_BUTTON_RIGHTSHOULDER,
            SDL_CONTROLLER_BUTTON_DPAD_UP,
            SDL_CONTROLLER_BUTTON_DPAD_DOWN,
            SDL_CONTROLLER_BUTTON_DPAD_LEFT,
            SDL_CONTROLLER_BUTTON_DPAD_RIGHT,
            SDL_CONTROLLER_BUTTON_MAX
        }

        /* CONSTANTS */

        public const byte SDL_QUERY = unchecked((byte) -1);
        public const byte SDL_IGNORE = 0;
        public const byte SDL_DISABLE = 0;
        public const byte SDL_ENABLE = 1;
        public const int SDL_TEXTEDITINGEVENT_TEXT_SIZE = 32;
        public const int SDL_TEXTINPUTEVENT_TEXT_SIZE = 32;
        public const uint SDL_SWSURFACE = 0;
        public const uint SDL_PREALLOC = 0x00000001;
        public const uint SDL_RLEACCEL = 0x00000002;
        public const uint SDL_DONTFREE = 0x00000004;
        public const uint SDL_TOUCH_MOUSEID = unchecked((uint) -1);
        public const int SDL_WINDOWPOS_UNDEFINED = 0x1FFF0000;
        public const int SDL_WINDOWPOS_CENTERED = 0x2FFF0000;
        public const int SDLK_SCANCODE_MASK = 1 << 30;

        /* STRUCTS */

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_version
        {
            public const int SizeInBytes = 3;
            public byte major;
            public byte minor;
            public byte patch;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Window
        {
            private IntPtr ptr;

            public SDL_Window(IntPtr ptr)
            {
                this.ptr = ptr;
            }

            public static implicit operator IntPtr(SDL_Window window)
            {
                return window.ptr;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_DisplayMode
        {
            public uint format;
            public int w;
            public int h;
            public int refreshrate;
            public IntPtr driverdata;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_TouchID
        {
            private long id;

            public SDL_TouchID(long id)
            {
                this.id = id;
            }

            public static implicit operator long(SDL_TouchID touchID)
            {
                return touchID.id;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_FingerID
        {
            private long id;

            public SDL_FingerID(long id)
            {
                this.id = id;
            }

            public static implicit operator long(SDL_FingerID fingerID)
            {
                return fingerID.id;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Finger
        {
            public SDL_FingerID id;
            public float x;
            public float y;
            public float pressure;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_SysWMinfo
        {
            public SDL_version version;
            public SDL_SYSWM_TYPE subsystem;
            public SDL_SysWMinfoUnion info;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct SDL_SysWMinfoUnion
        {
            [FieldOffset(0)] public SDL_SysWMinfoWin win;
            [FieldOffset(0)] public SDL_SysWMinfoX11 x11;
            [FieldOffset(0)] public SDL_SysWMinfoCocoa cocoa;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_SysWMinfoWin
        {
            public IntPtr window;
            public IntPtr hdc;
            public IntPtr hinstance;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_SysWMinfoCocoa
        {
            public IntPtr window;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_SysWMinfoX11
        {
            public IntPtr display;
            public IntPtr window;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Rect
        {
            public int x;
            public int y;
            public int w;
            public int h;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Point
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Surface
        {
            public uint flags;
            public SDL_PixelFormat* format;
            public int w;
            public int h;
            public int pitch;
            public void* pixels;
            public void* userdata;
            public int locked;
            public void* lock_data;
            public SDL_Rect clip_rect;
            public IntPtr map;
            public int refcount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_RendererInfo
        {
            public byte* name;
            public SDL_RendererFlags flags;
            public uint num_texture_formats;
            public fixed uint texture_formats[16];
            public int max_texture_width;
            public int max_texture_height;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Renderer
        {
            private IntPtr ptr;

            public SDL_Renderer(IntPtr ptr)
            {
                this.ptr = ptr;
            }

            public static implicit operator IntPtr(SDL_Renderer renderer)
            {
                return renderer.ptr;
            }
        }

        /*[StructLayout(LayoutKind.Sequential)]
        public struct SDL_GLContext
        {
            private IntPtr ptr;

            public SDL_GLContext(IntPtr ptr)
            {
                this.ptr = ptr;
            }

            public static implicit operator IntPtr(SDL_GLContext context)
            {
                return context.ptr;
            }
        }*/

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Texture
        {
            private IntPtr ptr;

            public SDL_Texture(IntPtr ptr)
            {
                this.ptr = ptr;
            }

            public static implicit operator IntPtr(SDL_Texture texture)
            {
                return texture.ptr;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Color
        {
            public byte r;
            public byte g;
            public byte b;
            public byte a;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Palette
        {
            public int ncolors;
            public SDL_Color* colors;
            public uint version;
            public int refcount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_PixelFormat
        {
            public uint format;
            public SDL_Palette* palette;
            public byte BitsPerPixel;
            public byte BytesPerPixel;
            public fixed byte padding[2];
            public uint Rmask;
            public uint Gmask;
            public uint Bmask;
            public uint Amask;
            public byte Rloss;
            public byte Gloss;
            public byte Bloss;
            public byte Aloss;
            public byte Rshift;
            public byte Gshift;
            public byte Bshift;
            public byte Ashift;
            public int refcount;
            public SDL_PixelFormat* next;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Cursor
        {
            private IntPtr ptr;

            public SDL_Cursor(IntPtr ptr)
            {
                this.ptr = ptr;
            }

            public static implicit operator IntPtr(SDL_Cursor cursor)
            {
                return cursor.ptr;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Keysym
        {
            public SDL_Scancode scancode;
            public SDL_Keycode sym;
            public SDL_Keymod mod;
            public uint unused;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Joystick
        {
            private IntPtr ptr;

            public SDL_Joystick(IntPtr ptr)
            {
                this.ptr = ptr;
            }

            public static implicit operator IntPtr(SDL_Joystick joystick)
            {
                return joystick.ptr;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_JoystickID
        {
            private int id;

            public SDL_JoystickID(int id)
            {
                this.id = id;
            }

            public static implicit operator int(SDL_JoystickID joystickID)
            {
                return joystickID.id;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_GameControllerButtonBind
        {
            public SDL_GameControllerBindType bindType;
            public SDL_GameControllerButtonBindUnion value;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct SDL_GameControllerButtonBindUnion
        {
            [FieldOffset(0)] public int button;
            [FieldOffset(0)] public int axis;
            [FieldOffset(0)] public Hat hat;

            [StructLayout(LayoutKind.Sequential)]
            public struct Hat
            {
                public int hat;
                public int hatMask;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_GameController
        {
            private IntPtr ptr;

            public SDL_GameController(IntPtr ptr)
            {
                this.ptr = ptr;
            }

            public static implicit operator IntPtr(SDL_GameController gameController)
            {
                return gameController.ptr;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_CommonEvent
        {
            public SDL_EventType type;
            public uint timestamp;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_WindowEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public uint windowID;
            public SDL_WindowEventID evt;
            private fixed byte padding[3];
            public int data1;
            public int data2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_KeyboardEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public uint windowID;
            public SDL_ButtonState state;
            public byte repeat;
            public byte padding2;
            public byte padding3;
            public SDL_Keysym keysym;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_TextEditingEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public uint windowID;
            public fixed byte text[SDL_TEXTEDITINGEVENT_TEXT_SIZE];
            public int start;
            public int length;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_TextInputEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public uint windowID;
            public fixed byte text[SDL_TEXTINPUTEVENT_TEXT_SIZE];
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_MouseMotionEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public uint windowID;
            public uint which;
            public SDL_ButtonMask state;
            public int x;
            public int y;
            public int xrel;
            public int yrel;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_MouseButtonEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public uint windowID;
            public uint which;
            public SDL_Button button;
            public SDL_ButtonState state;
            public byte clicks;
            public byte padding;
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_MouseWheelEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public uint windowID;
            public uint which;
            public int x;
            public int y;
            public SDL_MouseWheelDirection direction;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_JoyAxisEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            SDL_JoystickID which;
            public byte axis;
            private fixed byte padding[3];
            public short value;
            private ushort padding4;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_JoyBallEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public SDL_JoystickID which;
            public byte ball;
            private fixed byte padding[3];
            public short xrel;
            public short yrel;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_JoyHatEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public SDL_JoystickID which;
            public byte hat;
            public SDL_Hat value;
            private ushort padding;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_JoyButtonEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public SDL_JoystickID which;
            public byte button;
            public SDL_ButtonState state;
            private ushort padding;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_JoyDeviceEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public int which;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_ControllerAxisEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public SDL_JoystickID which;
            public SDL_GameControllerAxis axis;
            private fixed byte padding[3];
            public short value;
            private ushort padding4;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_ControllerButtonEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public SDL_JoystickID which;
            public SDL_GameControllerButton button;
            public SDL_ButtonState state;
            private ushort padding;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_ControllerDeviceEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public int which;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_AudioDeviceEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public uint which;
            public byte iscapture;
            private fixed byte padding[3];
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_TouchFingerEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public SDL_TouchID touchId;
            public SDL_FingerID fingerId;
            public float x;
            public float y;
            public float dx;
            public float dy;
            public float pressure;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_MultiGestureEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public SDL_TouchID touchId;
            public float dTheta;
            public float dDist;
            public float x;
            public float y;
            public ushort numFingers;
            private ushort padding;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_DollarGestureEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public SDL_TouchID touchId;
            public long gestureId;
            public uint numFingers;
            public float error;
            public float x;
            public float y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_DropEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public byte* file;
            public uint windowID;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_QuitEvent
        {
            public SDL_EventType type;
            public uint timestamp;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_OSEvent
        {
            public SDL_EventType type;
            public uint timestamp;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_UserEvent
        {
            public SDL_EventType type;
            public uint timestamp;
            public uint windowID;
            public int code;
            public void* data1;
            public void* data2;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct SDL_Event
        {
            [FieldOffset(0)] public SDL_EventType type;

            [FieldOffset(0)] public SDL_CommonEvent common;

            [FieldOffset(0)] public SDL_WindowEvent window;

            [FieldOffset(0)] public SDL_KeyboardEvent keyboard;

            [FieldOffset(0)] public SDL_TextEditingEvent edit;

            [FieldOffset(0)] public SDL_TextInputEvent text;

            [FieldOffset(0)] public SDL_MouseMotionEvent motion;

            [FieldOffset(0)] public SDL_MouseButtonEvent button;

            [FieldOffset(0)] public SDL_MouseWheelEvent wheel;

            [FieldOffset(0)] public SDL_JoyAxisEvent jaxis;

            [FieldOffset(0)] public SDL_JoyBallEvent jball;

            [FieldOffset(0)] public SDL_JoyHatEvent jhat;

            [FieldOffset(0)] public SDL_JoyButtonEvent jbutton;

            [FieldOffset(0)] public SDL_JoyDeviceEvent jdevice;

            [FieldOffset(0)] public SDL_ControllerAxisEvent caxis;

            [FieldOffset(0)] public SDL_ControllerButtonEvent cbutton;

            [FieldOffset(0)] public SDL_ControllerDeviceEvent cdevice;

            [FieldOffset(0)] public SDL_AudioDeviceEvent adevice;

            [FieldOffset(0)] public SDL_QuitEvent quit;

            [FieldOffset(0)] public SDL_UserEvent user;

            [FieldOffset(0)] public SDL_TouchFingerEvent tfinger;

            [FieldOffset(0)] public SDL_MultiGestureEvent mgesture;

            [FieldOffset(0)] public SDL_DollarGestureEvent dgesture;

            [FieldOffset(0)] public SDL_DropEvent drop;

            [FieldOffset(0)] private fixed byte padding[56];
        }


        /* METHODS */

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GetVersion_d(SDL_version* version);

        private static SDL_GetVersion_d SDL_GetVersion_f;

        public static void SDL_GetVersion(SDL_version* version) => SDL_GetVersion_f(version);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetRevision_d();

        private static SDL_GetRevision_d SDL_GetRevision_f;

        public static byte* SDL_GetRevision() => SDL_GetRevision_f();

        public static string SDL_GetRevisionString()
        {
            return GetString(SDL_GetRevision());
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetRevisionNumber_d();

        private static SDL_GetRevisionNumber_d SDL_GetRevisionNumber_f;

        public static int SDL_GetRevisionNumber() => SDL_GetRevisionNumber_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_Init_d(SDL_InitFlags flags);

        private static SDL_Init_d SDL_Init_f;

        public static int SDL_Init(SDL_InitFlags flags) => SDL_Init_f(flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_InitSubSystem_d(SDL_InitFlags flags);

        private static SDL_InitSubSystem_d SDL_InitSubSystem_f;

        public static int SDL_InitSubSystem(SDL_InitFlags flags) => SDL_InitSubSystem_f(flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_QuitSubSystem_d(SDL_InitFlags flags);

        private static SDL_QuitSubSystem_d SDL_QuitSubSystem_f;

        public static void SDL_QuitSubSystem(SDL_InitFlags flags) => SDL_QuitSubSystem_f(flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_InitFlags SDL_WasInit_d(SDL_InitFlags flags);

        private static SDL_WasInit_d SDL_WasInit_f;

        public static SDL_InitFlags SDL_WasInit(SDL_InitFlags flags) => SDL_WasInit_f(flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_Quit_d();

        private static SDL_Quit_d SDL_Quit_f;

        public static void SDL_Quit() => SDL_Quit_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate SDL_HitTestResult SDL_HitTest(SDL_Window window, SDL_Point* area, void* data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Window SDL_CreateWindow_d(
            string title, int x, int y, int width, int height, SDL_WindowFlags flags);

        private static SDL_CreateWindow_d SDL_CreateWindow_f;

        public static SDL_Window SDL_CreateWindow(string title, int x, int y, int width, int height,
            SDL_WindowFlags flags) => SDL_CreateWindow_f(title, x, y, width, height, flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Window SDL_CreateWindowFrom_d(IntPtr nativeWindow);

        private static SDL_CreateWindowFrom_d SDL_CreateWindowFrom_f;

        public static SDL_Window SDL_CreateWindowFrom(IntPtr nativeWindow) => SDL_CreateWindowFrom_f(nativeWindow);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Window SDL_DestroyWindow_d(SDL_Window window);

        private static SDL_DestroyWindow_d SDL_DestroyWindow_f;

        public static SDL_Window SDL_DestroyWindow(SDL_Window window) => SDL_DestroyWindow_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_DisableScreenSaver_d();

        private static SDL_DisableScreenSaver_d SDL_DisableScreenSaver_f;

        public static void SDL_DisableScreenSaver() => SDL_DisableScreenSaver_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_EnableScreenSaver_d();

        private static SDL_EnableScreenSaver_d SDL_EnableScreenSaver_f;

        public static void SDL_EnableScreenSaver() => SDL_EnableScreenSaver_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_DisplayMode* SDL_GetClosestDisplayMode_d(int displayIndex, SDL_DisplayMode* mode,
            SDL_DisplayMode* closest);

        private static SDL_GetClosestDisplayMode_d SDL_GetClosestDisplayMode_f;

        public static SDL_DisplayMode* SDL_GetClosestDisplayMode(int displayIndex, SDL_DisplayMode* mode,
            SDL_DisplayMode* closest) => SDL_GetClosestDisplayMode_f(displayIndex, mode, closest);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetCurrentDisplayMode_d(int displayIndex, SDL_DisplayMode* mode);

        private static SDL_GetCurrentDisplayMode_d SDL_GetCurrentDisplayMode_f;

        public static int SDL_GetCurrentDisplayMode(int displayIndex, SDL_DisplayMode* mode) =>
            SDL_GetCurrentDisplayMode_f(displayIndex, mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetCurrentVideoDriver_d();

        private static SDL_GetCurrentVideoDriver_d SDL_GetCurrentVideoDriver_f;

        public static byte* SDL_GetCurrentVideoDriver() => SDL_GetCurrentVideoDriver_f();

        public static string SDL_GetCurrentVideoDriverString()
        {
            return GetString(SDL_GetCurrentVideoDriver());
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetDesktopDisplayMode_d(int displayIndex, SDL_DisplayMode* mode);

        private static SDL_GetDesktopDisplayMode_d SDL_GetDesktopDisplayMode_f;

        public static int SDL_GetDesktopDisplayMode(int displayIndex, SDL_DisplayMode* mode) =>
            SDL_GetDesktopDisplayMode_f(displayIndex, mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetDisplayBounds_d(int displayIndex, SDL_Rect* rect);

        private static SDL_GetDisplayBounds_d SDL_GetDisplayBounds_f;

        public static int SDL_GetDisplayBounds(int displayIndex, SDL_Rect* rect) =>
            SDL_GetDisplayBounds_f(displayIndex, rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetDisplayDPI_d(int displayIndex, float* ddpi, float* hdpi, float* vdpi);

        private static SDL_GetDisplayDPI_d SDL_GetDisplayDPI_f;

        public static int SDL_GetDisplayDPI(int displayIndex, float* ddpi, float* hdpi, float* vdpi) =>
            SDL_GetDisplayDPI_f(displayIndex, ddpi, hdpi, vdpi);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetDisplayMode_d(int displayIndex, int modeIndex, SDL_DisplayMode* mode);

        private static SDL_GetDisplayMode_d SDL_GetDisplayMode_f;

        public static int SDL_GetDisplayMode(int displayIndex, int modeIndex, SDL_DisplayMode* mode) =>
            SDL_GetDisplayMode_f(displayIndex, modeIndex, mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetDisplayName_d(int displayIndex);

        private static SDL_GetDisplayName_d SDL_GetDisplayName_f;

        public static byte* SDL_GetDisplayName(int displayIndex) => SDL_GetDisplayName_f(displayIndex);

        public static string SDL_GetDisplayNameString(int displayIndex)
        {
            return GetString(SDL_GetDisplayName(displayIndex));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetDisplayUsableBounds_d(int displayIndex,
            SDL_Rect* rect);

        private static SDL_GetDisplayUsableBounds_d SDL_GetDisplayUsableBounds_f;

        public static int SDL_GetDisplayUsableBounds(int displayIndex, SDL_Rect* rect) =>
            SDL_GetDisplayUsableBounds_f(displayIndex, rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Window SDL_GetGrabbedWindow_d();

        private static SDL_GetGrabbedWindow_d SDL_GetGrabbedWindow_f;

        public static SDL_Window SDL_GetGrabbedWindow() => SDL_GetGrabbedWindow_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetNumDisplayModes_d(int displayIndex);

        private static SDL_GetNumDisplayModes_d SDL_GetNumDisplayModes_f;

        public static int SDL_GetNumDisplayModes(int displayIndex) => SDL_GetNumDisplayModes_f(displayIndex);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetNumVideoDisplays_d();

        private static SDL_GetNumVideoDisplays_d SDL_GetNumVideoDisplays_f;

        public static int SDL_GetNumVideoDisplays() => SDL_GetNumVideoDisplays_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetNumVideoDrivers_d();

        private static SDL_GetNumVideoDrivers_d SDL_GetNumVideoDrivers_f;

        public static int SDL_GetNumVideoDrivers() => SDL_GetNumVideoDrivers_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetVideoDriver_d(int index);

        private static SDL_GetVideoDriver_d SDL_GetVideoDriver_f;

        public static byte* SDL_GetVideoDriver(int index) => SDL_GetVideoDriver_f(index);

        public static string SDL_GetVideoDriverString(int index)
        {
            return GetString(SDL_GetVideoDriver(index));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetWindowBordersSize_d(SDL_Window window,
            int* top, int* left, int* bottom, int* right);

        private static SDL_GetWindowBordersSize_d SDL_GetWindowBordersSize_f;

        public static int SDL_GetWindowBordersSize(SDL_Window window, int* top, int* left, int* bottom, int* right) =>
            SDL_GetWindowBordersSize_f(window, top, left, bottom, right);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint SDL_GetWindowID_d(SDL_Window window);

        private static SDL_GetWindowID_d SDL_GetWindowID_f;

        public static uint SDL_GetWindowID(SDL_Window window) =>
            SDL_GetWindowID_f(window);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate float SDL_GetWindowBrightness_d(SDL_Window window);

        private static SDL_GetWindowBrightness_d SDL_GetWindowBrightness_f;

        public static float SDL_GetWindowBrightness(SDL_Window window) => SDL_GetWindowBrightness_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void* SDL_GetWindowData_d(SDL_Window window, string name);

        private static SDL_GetWindowData_d SDL_GetWindowData_f;

        public static void* SDL_GetWindowData(SDL_Window window, string name) => SDL_GetWindowData_f(window, name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetWindowDisplayIndex_d(SDL_Window window);

        private static SDL_GetWindowDisplayIndex_d SDL_GetWindowDisplayIndex_f;

        public static int SDL_GetWindowDisplayIndex(SDL_Window window) => SDL_GetWindowDisplayIndex_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetWindowDisplayMode_d(SDL_Window window, SDL_DisplayMode* mode);

        private static SDL_GetWindowDisplayMode_d SDL_GetWindowDisplayMode_f;

        public static int SDL_GetWindowDisplayMode(SDL_Window window, SDL_DisplayMode* mode) =>
            SDL_GetWindowDisplayMode_f(window, mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_WindowFlags SDL_GetWindowFlags_d(SDL_Window window);

        private static SDL_GetWindowFlags_d SDL_GetWindowFlags_f;

        public static SDL_WindowFlags SDL_GetWindowFlags(SDL_Window window) => SDL_GetWindowFlags_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Window SDL_GetWindowFromID_d(uint id);

        private static SDL_GetWindowFromID_d SDL_GetWindowFromID_f;

        public static SDL_Window SDL_GetWindowFromID(uint id) => SDL_GetWindowFromID_f(id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GetWindowMaximumSize_d(SDL_Window window, int* w, int* h);

        private static SDL_GetWindowMaximumSize_d SDL_GetWindowMaximumSize_f;

        public static void SDL_GetWindowMaximumSize(SDL_Window window, int* w, int* h) =>
            SDL_GetWindowMaximumSize_f(window, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GetWindowMinimumSize_d(SDL_Window window, int* w, int* h);

        private static SDL_GetWindowMinimumSize_d SDL_GetWindowMinimumSize_f;

        public static void SDL_GetWindowMinimumSize(SDL_Window window, int* w, int* h) =>
            SDL_GetWindowMinimumSize_f(window, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetWindowOpacity_d(SDL_Window window, float* opacity);

        private static SDL_GetWindowOpacity_d SDL_GetWindowOpacity_f;

        public static int SDL_GetWindowOpacity(SDL_Window window, float* opacity) =>
            SDL_GetWindowOpacity_f(window, opacity);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint SDL_GetWindowPixelFormat_d(SDL_Window window);

        private static SDL_GetWindowPixelFormat_d SDL_GetWindowPixelFormat_f;

        public static uint SDL_GetWindowPixelFormat(SDL_Window window) => SDL_GetWindowPixelFormat_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GetWindowPosition_d(SDL_Window window, int* x, int* y);

        private static SDL_GetWindowPosition_d SDL_GetWindowPosition_f;

        public static void SDL_GetWindowPosition(SDL_Window window, int* x, int* y) =>
            SDL_GetWindowPosition_f(window, x, y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GetWindowSize_d(SDL_Window window, int* w, int* h);

        private static SDL_GetWindowSize_d SDL_GetWindowSize_f;

        public static void SDL_GetWindowSize(SDL_Window window, int* w, int* h) => SDL_GetWindowSize_f(window, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetWindowTitle_d(SDL_Window window);

        private static SDL_GetWindowTitle_d SDL_GetWindowTitle_f;

        public static byte* SDL_GetWindowTitle(SDL_Window window) => SDL_GetWindowTitle_f(window);

        public static string SDL_GetWindowTitleString(SDL_Window window)
        {
            return GetString(SDL_GetWindowTitle(window));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_HideWindow_d(SDL_Window window);

        private static SDL_HideWindow_d SDL_HideWindow_f;

        public static void SDL_HideWindow(SDL_Window window) => SDL_HideWindow_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_IsScreenSaverEnabled_d();

        private static SDL_IsScreenSaverEnabled_d SDL_IsScreenSaverEnabled_f;

        public static bool SDL_IsScreenSaverEnabled() => SDL_IsScreenSaverEnabled_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_MaximizeWindow_d(SDL_Window window);

        private static SDL_MaximizeWindow_d SDL_MaximizeWindow_f;

        public static void SDL_MaximizeWindow(SDL_Window window) => SDL_MaximizeWindow_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_MinimizeWindow_d(SDL_Window window);

        private static SDL_MinimizeWindow_d SDL_MinimizeWindow_f;

        public static void SDL_MinimizeWindow(SDL_Window window) => SDL_MinimizeWindow_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_RaiseWindow_d(SDL_Window window);

        private static SDL_RaiseWindow_d SDL_RaiseWindow_f;

        public static void SDL_RaiseWindow(SDL_Window window) => SDL_RaiseWindow_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_RestoreWindow_d(SDL_Window window);

        private static SDL_RestoreWindow_d SDL_RestoreWindow_f;

        public static void SDL_RestoreWindow(SDL_Window window) => SDL_RestoreWindow_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetWindowBordered_d(SDL_Window window, bool bordered);

        private static SDL_SetWindowBordered_d SDL_SetWindowBordered_f;

        public static void SDL_SetWindowBordered(SDL_Window window, bool bordered) =>
            SDL_SetWindowBordered_f(window, bordered);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetWindowBrightness_d(SDL_Window window, float brightness);

        private static SDL_SetWindowBrightness_d SDL_SetWindowBrightness_f;

        public static int SDL_SetWindowBrightness(SDL_Window window, float brightness) =>
            SDL_SetWindowBrightness_f(window, brightness);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void* SDL_SetWindowData_d(SDL_Window window, string name, void* userdata);

        private static SDL_SetWindowData_d SDL_SetWindowData_f;

        public static void* SDL_SetWindowData(SDL_Window window, string name, void* userdata) =>
            SDL_SetWindowData_f(window, name, userdata);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetWindowDisplayMode_d(SDL_Window window, SDL_DisplayMode* mode);

        private static SDL_SetWindowDisplayMode_d SDL_SetWindowDisplayMode_f;

        public static int SDL_SetWindowDisplayMode(SDL_Window window, SDL_DisplayMode* mode) =>
            SDL_SetWindowDisplayMode_f(window, mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetWindowFullscreen_d(SDL_Window window, SDL_WindowFlags flags);

        private static SDL_SetWindowFullscreen_d SDL_SetWindowFullscreen_f;

        public static int SDL_SetWindowFullscreen(SDL_Window window, SDL_WindowFlags flags) =>
            SDL_SetWindowFullscreen_f(window, flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetWindowGammaRamp_d(SDL_Window window, ushort* red, ushort* green, ushort* blue);

        private static SDL_SetWindowGammaRamp_d SDL_SetWindowGammaRamp_f;

        public static int SDL_SetWindowGammaRamp(SDL_Window window, ushort* red, ushort* green, ushort* blue) =>
            SDL_SetWindowGammaRamp_f(window, red, green, blue);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetWindowGrab_d(SDL_Window window, bool grabbed);

        private static SDL_SetWindowGrab_d SDL_SetWindowGrab_f;

        public static void SDL_SetWindowGrab(SDL_Window window, bool grabbed) => SDL_SetWindowGrab_f(window, grabbed);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetWindowHitTest_d(SDL_Window window, SDL_HitTest callback, void* data);

        private static SDL_SetWindowHitTest_d SDL_SetWindowHitTest_f;

        public static int SDL_SetWindowHitTest(SDL_Window window, SDL_HitTest callback, void* data) =>
            SDL_SetWindowHitTest_f(window, callback, data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetWindowInputFocus_d(SDL_Window window);

        private static SDL_SetWindowInputFocus_d SDL_SetWindowInputFocus_f;

        public static int SDL_SetWindowInputFocus(SDL_Window window) => SDL_SetWindowInputFocus_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetWindowMaximumSize_d(SDL_Window window, int maxWidth, int maxHeight);

        private static SDL_SetWindowMaximumSize_d SDL_SetWindowMaximumSize_f;

        public static void SDL_SetWindowMaximumSize(SDL_Window window, int maxWidth, int maxHeight) =>
            SDL_SetWindowMaximumSize_f(window, maxWidth, maxHeight);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetWindowMinimumSize_d(SDL_Window window, int minWidth, int minHeight);

        private static SDL_SetWindowMinimumSize_d SDL_SetWindowMinimumSize_f;

        public static void SDL_SetWindowMinimumSize(SDL_Window window, int minWidth, int minHeight) =>
            SDL_SetWindowMinimumSize_f(window, minWidth, minHeight);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetWindowModalFor_d(SDL_Window modalWindow, SDL_Window parentWindow);

        private static SDL_SetWindowModalFor_d SDL_SetWindowModalFor_f;

        public static int SDL_SetWindowModalFor(SDL_Window modalWindow, SDL_Window parentWindow) =>
            SDL_SetWindowModalFor_f(modalWindow, parentWindow);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetWindowOpacity_d(SDL_Window window, float opacity);

        private static SDL_SetWindowOpacity_d SDL_SetWindowOpacity_f;

        public static int SDL_SetWindowOpacity(SDL_Window window, float opacity) =>
            SDL_SetWindowOpacity_f(window, opacity);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetWindowPosition_d(SDL_Window window, int x, int y);

        private static SDL_SetWindowPosition_d SDL_SetWindowPosition_f;

        public static void SDL_SetWindowPosition(SDL_Window window, int x, int y) =>
            SDL_SetWindowPosition_f(window, x, y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetWindowResizable_d(SDL_Window window, bool resizable);

        private static SDL_SetWindowResizable_d SDL_SetWindowResizable_f;

        public static void SDL_SetWindowResizable(SDL_Window window, bool resizable) =>
            SDL_SetWindowResizable_f(window, resizable);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetWindowSize_d(SDL_Window window, int width, int height);

        private static SDL_SetWindowSize_d SDL_SetWindowSize_f;

        public static void SDL_SetWindowSize(SDL_Window window, int width, int height) =>
            SDL_SetWindowSize_f(window, width, height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetWindowTitle_d(SDL_Window window, string title);

        private static SDL_SetWindowTitle_d SDL_SetWindowTitle_f;

        public static void SDL_SetWindowTitle(SDL_Window window, string title) => SDL_SetWindowTitle_f(window, title);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_ShowWindow_d(SDL_Window window);

        private static SDL_ShowWindow_d SDL_ShowWindow_f;

        public static void SDL_ShowWindow(SDL_Window window) => SDL_ShowWindow_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_VideoInit_d(string driverName);

        private static SDL_VideoInit_d SDL_VideoInit_f;

        public static int SDL_VideoInit(string driverName) => SDL_VideoInit_f(driverName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_VideoQuit_d();

        private static SDL_VideoQuit_d SDL_VideoQuit_f;

        public static void SDL_VideoQuit() => SDL_VideoQuit_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_GetWindowWMInfo_d(SDL_Window window, SDL_SysWMinfo* info);

        private static SDL_GetWindowWMInfo_d SDL_GetWindowWMInfo_f;

        public static bool SDL_GetWindowWMInfo(SDL_Window window, SDL_SysWMinfo* info) =>
            SDL_GetWindowWMInfo_f(window, info);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Renderer SDL_CreateRenderer_d(SDL_Window window, int index, SDL_RendererFlags flags);

        private static SDL_CreateRenderer_d SDL_CreateRenderer_f;

        public static SDL_Renderer SDL_CreateRenderer(SDL_Window window, int index, SDL_RendererFlags flags) =>
            SDL_CreateRenderer_f(window, index, flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Renderer SDL_CreateSoftwareRenderer_d(SDL_Surface* surface);

        private static SDL_CreateSoftwareRenderer_d SDL_CreateSoftwareRenderer_f;

        public static SDL_Renderer SDL_CreateSoftwareRenderer(SDL_Surface* surface) =>
            SDL_CreateSoftwareRenderer_f(surface);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Texture SDL_CreateTexture_d(SDL_Renderer renderer, uint format, SDL_TextureAccess access,
            int w, int h);

        private static SDL_CreateTexture_d SDL_CreateTexture_f;

        public static SDL_Texture SDL_CreateTexture(SDL_Renderer renderer, uint format, SDL_TextureAccess access, int w,
            int h) => SDL_CreateTexture_f(renderer, format, access, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Texture SDL_CreateTextureFromSurface_d(SDL_Renderer renderer, SDL_Surface* surface);

        private static SDL_CreateTextureFromSurface_d SDL_CreateTextureFromSurface_f;

        public static SDL_Texture SDL_CreateTextureFromSurface(SDL_Renderer renderer, SDL_Surface* surface) =>
            SDL_CreateTextureFromSurface_f(renderer, surface);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_CreateWindowAndRenderer_d(int width, int height, SDL_WindowFlags window_flags,
            SDL_Window* window, SDL_Renderer* renderer);

        private static SDL_CreateWindowAndRenderer_d SDL_CreateWindowAndRenderer_f;

        public static int SDL_CreateWindowAndRenderer(int width, int height, SDL_WindowFlags window_flags,
            SDL_Window* window, SDL_Renderer* renderer) =>
            SDL_CreateWindowAndRenderer_f(width, height, window_flags, window, renderer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_DestroyRenderer_d(SDL_Renderer renderer);

        private static SDL_DestroyRenderer_d SDL_DestroyRenderer_f;

        public static void SDL_DestroyRenderer(SDL_Renderer renderer) => SDL_DestroyRenderer_f(renderer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_DestroyTexture_d(SDL_Texture texture);

        private static SDL_DestroyTexture_d SDL_DestroyTexture_f;

        public static void SDL_DestroyTexture(SDL_Texture texture) => SDL_DestroyTexture_f(texture);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetNumRenderDrivers_d();

        private static SDL_GetNumRenderDrivers_d SDL_GetNumRenderDrivers_f;

        public static int SDL_GetNumRenderDrivers() => SDL_GetNumRenderDrivers_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetRenderDrawBlendMode_d(SDL_Renderer renderer, SDL_BlendMode* blendMode);

        private static SDL_GetRenderDrawBlendMode_d SDL_GetRenderDrawBlendMode_f;

        public static int SDL_GetRenderDrawBlendMode(SDL_Renderer renderer, SDL_BlendMode* blendMode) =>
            SDL_GetRenderDrawBlendMode_f(renderer, blendMode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetRenderDrawColor_d(SDL_Renderer renderer, byte* r, byte* g, byte* b, byte* a);

        private static SDL_GetRenderDrawColor_d SDL_GetRenderDrawColor_f;

        public static int SDL_GetRenderDrawColor(SDL_Renderer renderer, byte* r, byte* g, byte* b, byte* a) =>
            SDL_GetRenderDrawColor_f(renderer, r, g, b, a);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetRenderDriverInfo_d(int index, SDL_RendererInfo* info);

        private static SDL_GetRenderDriverInfo_d SDL_GetRenderDriverInfo_f;

        public static int SDL_GetRenderDriverInfo(int index, SDL_RendererInfo* info) =>
            SDL_GetRenderDriverInfo_f(index, info);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Texture SDL_GetRenderTarget_d(SDL_Renderer* renderer);

        private static SDL_GetRenderTarget_d SDL_GetRenderTarget_f;

        public static SDL_Texture SDL_GetRenderTarget(SDL_Renderer* renderer) => SDL_GetRenderTarget_f(renderer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Renderer* SDL_GetRenderer_d(SDL_Window window);

        private static SDL_GetRenderer_d SDL_GetRenderer_f;

        public static SDL_Renderer* SDL_GetRenderer(SDL_Window window) => SDL_GetRenderer_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetRendererInfo_d(SDL_Renderer renderer, SDL_RendererInfo* info);

        private static SDL_GetRendererInfo_d SDL_GetRendererInfo_f;

        public static int SDL_GetRendererInfo(SDL_Renderer renderer, SDL_RendererInfo* info) =>
            SDL_GetRendererInfo_f(renderer, info);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetRendererOutputSize_d(SDL_Renderer renderer, int* w, int* h);

        private static SDL_GetRendererOutputSize_d SDL_GetRendererOutputSize_f;

        public static int SDL_GetRendererOutputSize(SDL_Renderer renderer, int* w, int* h) =>
            SDL_GetRendererOutputSize_f(renderer, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetTextureAlphaMod_d(SDL_Texture texture, byte* alpha);

        private static SDL_GetTextureAlphaMod_d SDL_GetTextureAlphaMod_f;

        public static int SDL_GetTextureAlphaMod(SDL_Texture texture, byte* alpha) =>
            SDL_GetTextureAlphaMod_f(texture, alpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetTextureBlendMode_d(SDL_Texture texture, SDL_BlendMode* blendMode);

        private static SDL_GetTextureBlendMode_d SDL_GetTextureBlendMode_f;

        public static int SDL_GetTextureBlendMode(SDL_Texture texture, SDL_BlendMode* blendMode) =>
            SDL_GetTextureBlendMode_f(texture, blendMode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetTextureColorMod_d(SDL_Texture texture, byte* r, byte* g, byte* b);

        private static SDL_GetTextureColorMod_d SDL_GetTextureColorMod_f;

        public static int SDL_GetTextureColorMod(SDL_Texture texture, byte* r, byte* g, byte* b) =>
            SDL_GetTextureColorMod_f(texture, r, g, b);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_LockTexture_d(SDL_Texture texture, SDL_Rect* rect, void** pixels, int* pitch);

        private static SDL_LockTexture_d SDL_LockTexture_f;

        public static int SDL_LockTexture(SDL_Texture texture, SDL_Rect* rect, void** pixels, int* pitch) =>
            SDL_LockTexture_f(texture, rect, pixels, pitch);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_QueryTexture_d(SDL_Texture texture, uint* format, SDL_TextureAccess* access, int* w,
            int* h);

        private static SDL_QueryTexture_d SDL_QueryTexture_f;

        public static int
            SDL_QueryTexture(SDL_Texture texture, uint* format, SDL_TextureAccess* access, int* w, int* h) =>
            SDL_QueryTexture_f(texture, format, access, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderClear_d(SDL_Renderer renderer);

        private static SDL_RenderClear_d SDL_RenderClear_f;

        public static int SDL_RenderClear(SDL_Renderer renderer) => SDL_RenderClear_f(renderer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderCopy_d(SDL_Renderer renderer, SDL_Texture texture, SDL_Rect* srcrect,
            SDL_Rect* dstrect);

        private static SDL_RenderCopy_d SDL_RenderCopy_f;

        public static int SDL_RenderCopy(SDL_Renderer renderer, SDL_Texture texture, SDL_Rect* srcrect,
            SDL_Rect* dstrect) => SDL_RenderCopy_f(renderer, texture, srcrect, dstrect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderCopyEx_d(SDL_Renderer renderer, SDL_Texture texture, SDL_Rect* srcrect,
            SDL_Rect* dstrect, double angle, SDL_Point* center, SDL_RendererFlip flip);

        private static SDL_RenderCopyEx_d SDL_RenderCopyEx_f;

        public static int SDL_RenderCopyEx(SDL_Renderer renderer, SDL_Texture texture, SDL_Rect* srcrect,
            SDL_Rect* dstrect, double angle, SDL_Point* center, SDL_RendererFlip flip) =>
            SDL_RenderCopyEx_f(renderer, texture, srcrect, dstrect, angle, center, flip);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderDrawLine_d(SDL_Renderer renderer, int x1, int y1, int x2, int y2);

        private static SDL_RenderDrawLine_d SDL_RenderDrawLine_f;

        public static int SDL_RenderDrawLine(SDL_Renderer renderer, int x1, int y1, int x2, int y2) =>
            SDL_RenderDrawLine_f(renderer, x1, y1, x2, y2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderDrawLines_d(SDL_Renderer renderer, SDL_Point* points, int count);

        private static SDL_RenderDrawLines_d SDL_RenderDrawLines_f;

        public static int SDL_RenderDrawLines(SDL_Renderer renderer, SDL_Point* points, int count) =>
            SDL_RenderDrawLines_f(renderer, points, count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderDrawPoint_d(SDL_Renderer renderer, int x, int y);

        private static SDL_RenderDrawPoint_d SDL_RenderDrawPoint_f;

        public static int SDL_RenderDrawPoint(SDL_Renderer renderer, int x, int y) =>
            SDL_RenderDrawPoint_f(renderer, x, y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderDrawPoints_d(SDL_Renderer renderer, SDL_Point* points, int count);

        private static SDL_RenderDrawPoints_d SDL_RenderDrawPoints_f;

        public static int SDL_RenderDrawPoints(SDL_Renderer renderer, SDL_Point* points, int count) =>
            SDL_RenderDrawPoints_f(renderer, points, count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderDrawRect_d(SDL_Renderer renderer, SDL_Rect* rect);

        private static SDL_RenderDrawRect_d SDL_RenderDrawRect_f;

        public static int SDL_RenderDrawRect(SDL_Renderer renderer, SDL_Rect* rect) =>
            SDL_RenderDrawRect_f(renderer, rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderDrawRects_d(SDL_Renderer renderer, SDL_Rect* rects, int count);

        private static SDL_RenderDrawRects_d SDL_RenderDrawRects_f;

        public static int SDL_RenderDrawRects(SDL_Renderer renderer, SDL_Rect* rects, int count) =>
            SDL_RenderDrawRects_f(renderer, rects, count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderFillRect_d(SDL_Renderer renderer, SDL_Rect* rect);

        private static SDL_RenderFillRect_d SDL_RenderFillRect_f;

        public static int SDL_RenderFillRect(SDL_Renderer renderer, SDL_Rect* rect) =>
            SDL_RenderFillRect_f(renderer, rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderFillRects_d(SDL_Renderer renderer, SDL_Rect* rects, int count);

        private static SDL_RenderFillRects_d SDL_RenderFillRects_f;

        public static int SDL_RenderFillRects(SDL_Renderer renderer, SDL_Rect* rects, int count) =>
            SDL_RenderFillRects_f(renderer, rects, count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_RenderGetClipRect_d(SDL_Renderer renderer, SDL_Rect* rect);

        private static SDL_RenderGetClipRect_d SDL_RenderGetClipRect_f;

        public static void SDL_RenderGetClipRect(SDL_Renderer renderer, SDL_Rect* rect) =>
            SDL_RenderGetClipRect_f(renderer, rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_RenderGetIntegerScale_d(SDL_Renderer renderer);

        private static SDL_RenderGetIntegerScale_d SDL_RenderGetIntegerScale_f;

        public static bool SDL_RenderGetIntegerScale(SDL_Renderer renderer) => SDL_RenderGetIntegerScale_f(renderer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_RenderGetLogicalSize_d(SDL_Renderer renderer, int* w, int* h);

        private static SDL_RenderGetLogicalSize_d SDL_RenderGetLogicalSize_f;

        public static void SDL_RenderGetLogicalSize(SDL_Renderer renderer, int* w, int* h) =>
            SDL_RenderGetLogicalSize_f(renderer, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_RenderGetScale_d(SDL_Renderer renderer, float* scaleX, float* scaleY);

        private static SDL_RenderGetScale_d SDL_RenderGetScale_f;

        public static void SDL_RenderGetScale(SDL_Renderer renderer, float* scaleX, float* scaleY) =>
            SDL_RenderGetScale_f(renderer, scaleX, scaleY);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_RenderGetViewport_d(SDL_Renderer renderer, SDL_Rect* rect);

        private static SDL_RenderGetViewport_d SDL_RenderGetViewport_f;

        public static void SDL_RenderGetViewport(SDL_Renderer renderer, SDL_Rect* rect) =>
            SDL_RenderGetViewport_f(renderer, rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_RenderIsClipEnabled_d(SDL_Renderer renderer);

        private static SDL_RenderIsClipEnabled_d SDL_RenderIsClipEnabled_f;

        public static bool SDL_RenderIsClipEnabled(SDL_Renderer renderer) => SDL_RenderIsClipEnabled_f(renderer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_RenderPresent_d(SDL_Renderer renderer);

        private static SDL_RenderPresent_d SDL_RenderPresent_f;

        public static void SDL_RenderPresent(SDL_Renderer renderer) => SDL_RenderPresent_f(renderer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderReadPixels_d(SDL_Renderer renderer, SDL_Rect* rect, uint format, void* pixels,
            int pitch);

        private static SDL_RenderReadPixels_d SDL_RenderReadPixels_f;

        public static int SDL_RenderReadPixels(SDL_Renderer renderer, SDL_Rect* rect, uint format, void* pixels,
            int pitch) => SDL_RenderReadPixels_f(renderer, rect, format, pixels, pitch);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderSetClipRect_d(SDL_Renderer renderer, SDL_Rect* rect);

        private static SDL_RenderSetClipRect_d SDL_RenderSetClipRect_f;

        public static int SDL_RenderSetClipRect(SDL_Renderer renderer, SDL_Rect* rect) =>
            SDL_RenderSetClipRect_f(renderer, rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderSetIntegerScale_d(SDL_Renderer renderer, bool enable);

        private static SDL_RenderSetIntegerScale_d SDL_RenderSetIntegerScale_f;

        public static int SDL_RenderSetIntegerScale(SDL_Renderer renderer, bool enable) =>
            SDL_RenderSetIntegerScale_f(renderer, enable);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderSetLogicalSize_d(SDL_Renderer renderer, int w, int h);

        private static SDL_RenderSetLogicalSize_d SDL_RenderSetLogicalSize_f;

        public static int SDL_RenderSetLogicalSize(SDL_Renderer renderer, int w, int h) =>
            SDL_RenderSetLogicalSize_f(renderer, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderSetScale_d(SDL_Renderer renderer, float scaleX, float scaleY);

        private static SDL_RenderSetScale_d SDL_RenderSetScale_f;

        public static int SDL_RenderSetScale(SDL_Renderer renderer, float scaleX, float scaleY) =>
            SDL_RenderSetScale_f(renderer, scaleX, scaleY);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_RenderSetViewport_d(SDL_Renderer renderer, SDL_Rect* rect);

        private static SDL_RenderSetViewport_d SDL_RenderSetViewport_f;

        public static int SDL_RenderSetViewport(SDL_Renderer renderer, SDL_Rect* rect) =>
            SDL_RenderSetViewport_f(renderer, rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_RenderTargetSupported_d(SDL_Renderer renderer);

        private static SDL_RenderTargetSupported_d SDL_RenderTargetSupported_f;

        public static bool SDL_RenderTargetSupported(SDL_Renderer renderer) => SDL_RenderTargetSupported_f(renderer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetRenderDrawBlendMode_d(SDL_Renderer renderer, SDL_BlendMode blendMode);

        private static SDL_SetRenderDrawBlendMode_d SDL_SetRenderDrawBlendMode_f;

        public static int SDL_SetRenderDrawBlendMode(SDL_Renderer renderer, SDL_BlendMode blendMode) =>
            SDL_SetRenderDrawBlendMode_f(renderer, blendMode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetRenderDrawColor_d(SDL_Renderer renderer, byte r, byte g, byte b, byte a);

        private static SDL_SetRenderDrawColor_d SDL_SetRenderDrawColor_f;

        public static int SDL_SetRenderDrawColor(SDL_Renderer renderer, byte r, byte g, byte b, byte a) =>
            SDL_SetRenderDrawColor_f(renderer, r, g, b, a);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetRenderTarget_d(SDL_Renderer renderer, SDL_Texture texture);

        private static SDL_SetRenderTarget_d SDL_SetRenderTarget_f;

        public static int SDL_SetRenderTarget(SDL_Renderer renderer, SDL_Texture texture) =>
            SDL_SetRenderTarget_f(renderer, texture);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetTextureAlphaMod_d(SDL_Texture texture, byte alpha);

        private static SDL_SetTextureAlphaMod_d SDL_SetTextureAlphaMod_f;

        public static int SDL_SetTextureAlphaMod(SDL_Texture texture, byte alpha) =>
            SDL_SetTextureAlphaMod_f(texture, alpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetTextureBlendMode_d(SDL_Texture texture, SDL_BlendMode blendMode);

        private static SDL_SetTextureBlendMode_d SDL_SetTextureBlendMode_f;

        public static int SDL_SetTextureBlendMode(SDL_Texture texture, SDL_BlendMode blendMode) =>
            SDL_SetTextureBlendMode_f(texture, blendMode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetTextureColorMod_d(SDL_Texture texture, byte r, byte g, byte b);

        private static SDL_SetTextureColorMod_d SDL_SetTextureColorMod_f;

        public static int SDL_SetTextureColorMod(SDL_Texture texture, byte r, byte g, byte b) =>
            SDL_SetTextureColorMod_f(texture, r, g, b);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_UnlockTexture_d(SDL_Texture texture);

        private static SDL_UnlockTexture_d SDL_UnlockTexture_f;

        public static void SDL_UnlockTexture(SDL_Texture texture) => SDL_UnlockTexture_f(texture);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_UpdateTexture_d(SDL_Texture texture, SDL_Rect* rect, void* pixels, int pitch);

        private static SDL_UpdateTexture_d SDL_UpdateTexture_f;

        public static int SDL_UpdateTexture(SDL_Texture texture, SDL_Rect* rect, void* pixels, int pitch) =>
            SDL_UpdateTexture_f(texture, rect, pixels, pitch);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_UpdateYUVTexture_d(SDL_Texture texture, SDL_Rect* rect, byte* Yplane, int Ypitch,
            byte* Uplane, int Upitch, byte* Vplane, int Vpitch);

        private static SDL_UpdateYUVTexture_d SDL_UpdateYUVTexture_f;

        public static int SDL_UpdateYUVTexture(SDL_Texture texture, SDL_Rect* rect, byte* Yplane, int Ypitch,
            byte* Uplane, int Upitch, byte* Vplane, int Vpitch) =>
            SDL_UpdateYUVTexture_f(texture, rect, Yplane, Ypitch, Uplane, Upitch, Vplane, Vpitch);

        public static int SDL_BlitScaled(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect) =>
            SDL_UpperBlitScaled_f(src, srcrect, dst, dstrect);

        public static int SDL_BlitSurface(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect) =>
            SDL_UpperBlit(src, srcrect, dst, dstrect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_ConvertPixels_d(int width, int height, uint src_format, void* src, int src_pitch,
            uint dst_format, void* dst, int dst_pitch);

        private static SDL_ConvertPixels_d SDL_ConvertPixels_f;

        public static int SDL_ConvertPixels(int width, int height, uint src_format, void* src, int src_pitch,
            uint dst_format, void* dst, int dst_pitch) => SDL_ConvertPixels_f(width, height, src_format, src, src_pitch,
            dst_format, dst, dst_pitch);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Surface* SDL_ConvertSurface_d(SDL_Surface* src, SDL_PixelFormat* fmt, uint flags);

        private static SDL_ConvertSurface_d SDL_ConvertSurface_f;

        public static SDL_Surface* SDL_ConvertSurface(SDL_Surface* src, SDL_PixelFormat* fmt, uint flags) =>
            SDL_ConvertSurface_f(src, fmt, flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Surface* SDL_ConvertSurfaceFormat_d(SDL_Surface* src, uint pixel_format, uint flags);

        private static SDL_ConvertSurfaceFormat_d SDL_ConvertSurfaceFormat_f;

        public static SDL_Surface* SDL_ConvertSurfaceFormat(SDL_Surface* src, uint pixel_format, uint flags) =>
            SDL_ConvertSurfaceFormat_f(src, pixel_format, flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Surface* SDL_CreateRGBSurface_d(uint flags, int width, int height, int depth, uint Rmask,
            uint Gmask, uint Bmask, uint Amask);

        private static SDL_CreateRGBSurface_d SDL_CreateRGBSurface_f;

        public static SDL_Surface* SDL_CreateRGBSurface(uint flags, int width, int height, int depth, uint Rmask,
            uint Gmask, uint Bmask, uint Amask) =>
            SDL_CreateRGBSurface_f(flags, width, height, depth, Rmask, Gmask, Bmask, Amask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Surface* SDL_CreateRGBSurfaceFrom_d(void* pixels, int width, int height, int depth,
            int pitch, uint Rmask, uint Gmask, uint Bmask, uint Amask);

        private static SDL_CreateRGBSurfaceFrom_d SDL_CreateRGBSurfaceFrom_f;

        public static SDL_Surface* SDL_CreateRGBSurfaceFrom(void* pixels, int width, int height, int depth, int pitch,
            uint Rmask, uint Gmask, uint Bmask, uint Amask) =>
            SDL_CreateRGBSurfaceFrom_f(pixels, width, height, depth, pitch, Rmask, Gmask, Bmask, Amask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Surface* SDL_CreateRGBSurfaceWithFormat_d(uint flags, int width, int height, int depth,
            uint format);

        private static SDL_CreateRGBSurfaceWithFormat_d SDL_CreateRGBSurfaceWithFormat_f;

        public static SDL_Surface* SDL_CreateRGBSurfaceWithFormat(uint flags, int width, int height, int depth,
            uint format) => SDL_CreateRGBSurfaceWithFormat_f(flags, width, height, depth, format);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Surface* SDL_CreateRGBSurfaceWithFormatFrom_d(void* pixels, int width, int height,
            int depth, int pitch, uint format);

        private static SDL_CreateRGBSurfaceWithFormatFrom_d SDL_CreateRGBSurfaceWithFormatFrom_f;

        public static SDL_Surface* SDL_CreateRGBSurfaceWithFormatFrom(void* pixels, int width, int height, int depth,
            int pitch, uint format) =>
            SDL_CreateRGBSurfaceWithFormatFrom_f(pixels, width, height, depth, pitch, format);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_FillRect_d(SDL_Surface* dst, SDL_Rect* rect, uint color);

        private static SDL_FillRect_d SDL_FillRect_f;

        public static int SDL_FillRect(SDL_Surface* dst, SDL_Rect* rect, uint color) =>
            SDL_FillRect_f(dst, rect, color);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_FillRects_d(SDL_Surface* dst, SDL_Rect* rects, int count, uint color);

        private static SDL_FillRects_d SDL_FillRects_f;

        public static int SDL_FillRects(SDL_Surface* dst, SDL_Rect* rects, int count, uint color) =>
            SDL_FillRects_f(dst, rects, count, color);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_FreeSurface_d(SDL_Surface* surface);

        private static SDL_FreeSurface_d SDL_FreeSurface_f;

        public static void SDL_FreeSurface(SDL_Surface* surface) => SDL_FreeSurface_f(surface);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GetClipRect_d(SDL_Surface* surface, SDL_Rect* rect);

        private static SDL_GetClipRect_d SDL_GetClipRect_f;

        public static void SDL_GetClipRect(SDL_Surface* surface, SDL_Rect* rect) => SDL_GetClipRect_f(surface, rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetColorKey_d(SDL_Surface* surface, uint* key);

        private static SDL_GetColorKey_d SDL_GetColorKey_f;

        public static int SDL_GetColorKey(SDL_Surface* surface, uint* key) => SDL_GetColorKey_f(surface, key);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetSurfaceAlphaMod_d(SDL_Surface* surface, byte* alpha);

        private static SDL_GetSurfaceAlphaMod_d SDL_GetSurfaceAlphaMod_f;

        public static int SDL_GetSurfaceAlphaMod(SDL_Surface* surface, byte* alpha) =>
            SDL_GetSurfaceAlphaMod_f(surface, alpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetSurfaceBlendMode_d(SDL_Surface* surface, SDL_BlendMode* blendMode);

        private static SDL_GetSurfaceBlendMode_d SDL_GetSurfaceBlendMode_f;

        public static int SDL_GetSurfaceBlendMode(SDL_Surface* surface, SDL_BlendMode* blendMode) =>
            SDL_GetSurfaceBlendMode_f(surface, blendMode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetSurfaceColorMod_d(SDL_Surface* surface, byte* r, byte* g, byte* b);

        private static SDL_GetSurfaceColorMod_d SDL_GetSurfaceColorMod_f;

        public static int SDL_GetSurfaceColorMod(SDL_Surface* surface, byte* r, byte* g, byte* b) =>
            SDL_GetSurfaceColorMod_f(surface, r, g, b);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_LockSurface_d(SDL_Surface* surface);

        private static SDL_LockSurface_d SDL_LockSurface_f;

        public static int SDL_LockSurface(SDL_Surface* surface) => SDL_LockSurface_f(surface);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_LowerBlit_d(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect);

        private static SDL_LowerBlit_d SDL_LowerBlit_f;

        public static int SDL_LowerBlit(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect) =>
            SDL_LowerBlit_f(src, srcrect, dst, dstrect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_LowerBlitScaled_d(SDL_Surface* surface, SDL_Rect* srcrect, SDL_Surface* dst,
            SDL_Rect* dstrect);

        private static SDL_LowerBlitScaled_d SDL_LowerBlitScaled_f;

        public static int SDL_LowerBlitScaled(SDL_Surface* surface, SDL_Rect* srcrect, SDL_Surface* dst,
            SDL_Rect* dstrect) => SDL_LowerBlitScaled_f(surface, srcrect, dst, dstrect);

        public static bool SDL_MUSTLOCK(SDL_Surface* s) => (s->flags & 0x00000002) != 0;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_SetClipRect_d(SDL_Surface* surface, SDL_Rect* rect);

        private static SDL_SetClipRect_d SDL_SetClipRect_f;

        public static bool SDL_SetClipRect(SDL_Surface* surface, SDL_Rect* rect) => SDL_SetClipRect_f(surface, rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetColorKey_d(SDL_Surface* surface, bool flag, uint key);

        private static SDL_SetColorKey_d SDL_SetColorKey_f;

        public static int SDL_SetColorKey(SDL_Surface* surface, bool flag, uint key) =>
            SDL_SetColorKey_f(surface, flag, key);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetSurfaceAlphaMod_d(SDL_Surface* surface, byte alpha);

        private static SDL_SetSurfaceAlphaMod_d SDL_SetSurfaceAlphaMod_f;

        public static int SDL_SetSurfaceAlphaMod(SDL_Surface* surface, byte alpha) =>
            SDL_SetSurfaceAlphaMod_f(surface, alpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetSurfaceBlendMode_d(SDL_Surface* surface, SDL_BlendMode blendMode);

        private static SDL_SetSurfaceBlendMode_d SDL_SetSurfaceBlendMode_f;

        public static int SDL_SetSurfaceBlendMode(SDL_Surface* surface, SDL_BlendMode blendMode) =>
            SDL_SetSurfaceBlendMode_f(surface, blendMode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetSurfaceColorMod_d(SDL_Surface* surface, byte r, byte g, byte b);

        private static SDL_SetSurfaceColorMod_d SDL_SetSurfaceColorMod_f;

        public static int SDL_SetSurfaceColorMod(SDL_Surface* surface, byte r, byte g, byte b) =>
            SDL_SetSurfaceColorMod_f(surface, r, g, b);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetSurfacePalette_d(SDL_Surface* surface, SDL_Palette* palette);

        private static SDL_SetSurfacePalette_d SDL_SetSurfacePalette_f;

        public static int SDL_SetSurfacePalette(SDL_Surface* surface, SDL_Palette* palette) =>
            SDL_SetSurfacePalette_f(surface, palette);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetSurfaceRLE_d(SDL_Surface* surface, int flag);

        private static SDL_SetSurfaceRLE_d SDL_SetSurfaceRLE_f;

        public static int SDL_SetSurfaceRLE(SDL_Surface* surface, int flag) => SDL_SetSurfaceRLE_f(surface, flag);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_UnlockSurface_d(SDL_Surface* surface);

        private static SDL_UnlockSurface_d SDL_UnlockSurface_f;

        public static void SDL_UnlockSurface(SDL_Surface* surface) => SDL_UnlockSurface_f(surface);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_UpperBlit_d(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect);

        private static SDL_UpperBlit_d SDL_UpperBlit_f;

        public static int SDL_UpperBlit(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect) =>
            SDL_UpperBlit_f(src, srcrect, dst, dstrect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_UpperBlitScaled_d(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst,
            SDL_Rect* dstrect);

        private static SDL_UpperBlitScaled_d SDL_UpperBlitScaled_f;

        public static int
            SDL_UpperBlitScaled(SDL_Surface* src, SDL_Rect* srcrect, SDL_Surface* dst, SDL_Rect* dstrect) =>
            SDL_UpperBlitScaled_f(src, srcrect, dst, dstrect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_PixelFormat* SDL_AllocFormat_d(uint pixel_format);

        private static SDL_AllocFormat_d SDL_AllocFormat_f;

        public static SDL_PixelFormat* SDL_AllocFormat(uint pixel_format) => SDL_AllocFormat_f(pixel_format);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Palette* SDL_AllocPalette_d(int ncolors);

        private static SDL_AllocPalette_d SDL_AllocPalette_f;

        public static SDL_Palette* SDL_AllocPalette(int ncolors) => SDL_AllocPalette_f(ncolors);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_CalculateGammaRamp_d(float gamma, ushort* ramp);

        private static SDL_CalculateGammaRamp_d SDL_CalculateGammaRamp_f;

        public static void SDL_CalculateGammaRamp(float gamma, ushort* ramp) => SDL_CalculateGammaRamp_f(gamma, ramp);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_FreeFormat_d(SDL_PixelFormat* format);

        private static SDL_FreeFormat_d SDL_FreeFormat_f;

        public static void SDL_FreeFormat(SDL_PixelFormat* format) => SDL_FreeFormat_f(format);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_FreePalette_d(SDL_Palette* palette);

        private static SDL_FreePalette_d SDL_FreePalette_f;

        public static void SDL_FreePalette(SDL_Palette* palette) => SDL_FreePalette_f(palette);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetPixelFormatName_d(uint format);

        private static SDL_GetPixelFormatName_d SDL_GetPixelFormatName_f;

        public static byte* SDL_GetPixelFormatName(uint format) => SDL_GetPixelFormatName_f(format);

        public static string SDL_GetPixelFormatNameString(uint format) => GetString(SDL_GetPixelFormatName(format));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GetRGB_d(uint pixel, SDL_PixelFormat* format, byte* r, byte* g, byte* b);

        private static SDL_GetRGB_d SDL_GetRGB_f;

        public static void SDL_GetRGB(uint pixel, SDL_PixelFormat* format, byte* r, byte* g, byte* b) =>
            SDL_GetRGB_f(pixel, format, r, g, b);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GetRGBA_d(uint pixel, SDL_PixelFormat* format, byte* r, byte* g, byte* b, byte* a);

        private static SDL_GetRGBA_d SDL_GetRGBA_f;

        public static void SDL_GetRGBA(uint pixel, SDL_PixelFormat* format, byte* r, byte* g, byte* b, byte* a) =>
            SDL_GetRGBA_f(pixel, format, r, g, b, a);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint SDL_MapRGB_d(SDL_PixelFormat* format, byte r, byte g, byte b);

        private static SDL_MapRGB_d SDL_MapRGB_f;

        public static uint SDL_MapRGB(SDL_PixelFormat* format, byte r, byte g, byte b) => SDL_MapRGB_f(format, r, g, b);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint SDL_MapRGBA_d(SDL_PixelFormat* format, byte r, byte g, byte b, byte a);

        private static SDL_MapRGBA_d SDL_MapRGBA_f;

        public static uint SDL_MapRGBA(SDL_PixelFormat* format, byte r, byte g, byte b, byte a) =>
            SDL_MapRGBA_f(format, r, g, b, a);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint SDL_MasksToPixelFormatEnum_d(int bpp, uint Rmask, uint Gmask, uint Bmask, uint Amask);

        private static SDL_MasksToPixelFormatEnum_d SDL_MasksToPixelFormatEnum_f;

        public static uint SDL_MasksToPixelFormatEnum(int bpp, uint Rmask, uint Gmask, uint Bmask, uint Amask) =>
            SDL_MasksToPixelFormatEnum_f(bpp, Rmask, Gmask, Bmask, Amask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_PixelFormatEnumToMasks_d(uint format, int* bpp, uint* Rmask, uint* Gmask, uint* Bmask,
            uint* Amask);

        private static SDL_PixelFormatEnumToMasks_d SDL_PixelFormatEnumToMasks_f;

        public static bool SDL_PixelFormatEnumToMasks(uint format, int* bpp, uint* Rmask, uint* Gmask, uint* Bmask,
            uint* Amask) => SDL_PixelFormatEnumToMasks_f(format, bpp, Rmask, Gmask, Bmask, Amask);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetPaletteColors_d(SDL_Palette* palette, SDL_Color* colors, int firstcolor,
            int ncolors);

        private static SDL_SetPaletteColors_d SDL_SetPaletteColors_f;

        public static int SDL_SetPaletteColors(SDL_Palette* palette, SDL_Color* colors, int firstcolor, int ncolors) =>
            SDL_SetPaletteColors_f(palette, colors, firstcolor, ncolors);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetPixelFormatPalette_d(SDL_PixelFormat* format, SDL_Palette* palette);

        private static SDL_SetPixelFormatPalette_d SDL_SetPixelFormatPalette_f;

        public static int SDL_SetPixelFormatPalette(SDL_PixelFormat* format, SDL_Palette* palette) =>
            SDL_SetPixelFormatPalette_f(format, palette);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GL_BindTexture_d(SDL_Texture texture, float* texw, float* texh);

        private static SDL_GL_BindTexture_d SDL_GL_BindTexture_f;

        public static int SDL_GL_BindTexture(SDL_Texture texture, float* texw, float* texh) =>
            SDL_GL_BindTexture_f(texture, texw, texh);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr SDL_GL_CreateContext_d(SDL_Window window);

        private static SDL_GL_CreateContext_d SDL_GL_CreateContext_f;

        public static IntPtr SDL_GL_CreateContext(SDL_Window window) => SDL_GL_CreateContext_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GL_DeleteContext_d(IntPtr context);

        private static SDL_GL_DeleteContext_d SDL_GL_DeleteContext_f;

        public static void SDL_GL_DeleteContext(IntPtr context) => SDL_GL_DeleteContext_f(context);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_GL_ExtensionSupported_d(string extension);

        private static SDL_GL_ExtensionSupported_d SDL_GL_ExtensionSupported_f;

        public static bool SDL_GL_ExtensionSupported(string extension) => SDL_GL_ExtensionSupported_f(extension);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GL_GetAttribute_d(SDL_GLattr attr, int* value);

        private static SDL_GL_GetAttribute_d SDL_GL_GetAttribute_f;

        public static int SDL_GL_GetAttribute(SDL_GLattr attr, int* value) => SDL_GL_GetAttribute_f(attr, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr SDL_GL_GetCurrentContext_d();

        private static SDL_GL_GetCurrentContext_d SDL_GL_GetCurrentContext_f;

        public static IntPtr SDL_GL_GetCurrentContext() => SDL_GL_GetCurrentContext_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Window SDL_GL_GetCurrentWindow_d();

        private static SDL_GL_GetCurrentWindow_d SDL_GL_GetCurrentWindow_f;

        public static SDL_Window SDL_GL_GetCurrentWindow() => SDL_GL_GetCurrentWindow_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GL_GetDrawableSize_d(SDL_Window window, int* width, int* height);

        private static SDL_GL_GetDrawableSize_d SDL_GL_GetDrawableSize_f;

        public static void SDL_GL_GetDrawableSize(SDL_Window window, int* width, int* height) =>
            SDL_GL_GetDrawableSize_f(window, width, height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr SDL_GL_GetProcAddress_d(string proc);

        private static SDL_GL_GetProcAddress_d SDL_GL_GetProcAddress_f;

        public static IntPtr SDL_GL_GetProcAddress(string proc) => SDL_GL_GetProcAddress_f(proc);

        public static T SDL_GL_GetProcDelegate<T>(string proc) where T : class
        {
            return Marshal.GetDelegateForFunctionPointer<T>(SDL_GL_GetProcAddress(proc));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_BlendMode SDL_ComposeCustomBlendMode_d(
            SDL_BlendFactor srcColorFactor,
            SDL_BlendFactor dstColorFactor,
            SDL_BlendOperation colorOperation,
            SDL_BlendFactor srcAlphaFactor,
            SDL_BlendFactor dstAlphaFactor,
            SDL_BlendOperation alphaOperation);

        private static SDL_ComposeCustomBlendMode_d SDL_ComposeCustomBlendMode_f;

        public static SDL_BlendMode SDL_ComposeCustomBlendMode(
            SDL_BlendFactor srcColorFactor,
            SDL_BlendFactor dstColorFactor,
            SDL_BlendOperation colorOperation,
            SDL_BlendFactor srcAlphaFactor,
            SDL_BlendFactor dstAlphaFactor,
            SDL_BlendOperation alphaOperation) => SDL_ComposeCustomBlendMode_f(
            srcColorFactor,
            dstColorFactor,
            colorOperation,
            srcAlphaFactor,
            dstAlphaFactor,
            alphaOperation);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GL_GetSwapInterval_d();

        private static SDL_GL_GetSwapInterval_d SDL_GL_GetSwapInterval_f;

        public static int SDL_GL_GetSwapInterval() => SDL_GL_GetSwapInterval_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GL_LoadLibrary_d(string path);

        private static SDL_GL_LoadLibrary_d SDL_GL_LoadLibrary_f;

        public static int SDL_GL_LoadLibrary(string path) => SDL_GL_LoadLibrary_f(path);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GL_MakeCurrent_d(SDL_Window window, IntPtr context);

        private static SDL_GL_MakeCurrent_d SDL_GL_MakeCurrent_f;

        public static int SDL_GL_MakeCurrent(SDL_Window window, IntPtr context) =>
            SDL_GL_MakeCurrent_f(window, context);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GL_ResetAttributes_d();

        private static SDL_GL_ResetAttributes_d SDL_GL_ResetAttributes_f;

        public static void SDL_GL_ResetAttributes() => SDL_GL_ResetAttributes_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GL_SetAttribute_d(SDL_GLattr attr, int value);

        private static SDL_GL_SetAttribute_d SDL_GL_SetAttribute_f;

        public static int SDL_GL_SetAttribute(SDL_GLattr attr, int value) => SDL_GL_SetAttribute_f(attr, value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GL_SetSwapInterval_d(int interval);

        private static SDL_GL_SetSwapInterval_d SDL_GL_SetSwapInterval_f;

        public static int SDL_GL_SetSwapInterval(int interval) => SDL_GL_SetSwapInterval_f(interval);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GL_SwapWindow_d(SDL_Window window);

        private static SDL_GL_SwapWindow_d SDL_GL_SwapWindow_f;

        public static void SDL_GL_SwapWindow(SDL_Window window) => SDL_GL_SwapWindow_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GL_UnbindTexture_d(SDL_Texture texture);

        private static SDL_GL_UnbindTexture_d SDL_GL_UnbindTexture_f;

        public static int SDL_GL_UnbindTexture(SDL_Texture texture) => SDL_GL_UnbindTexture_f(texture);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GL_UnloadLibrary_d();

        private static SDL_GL_UnloadLibrary_d SDL_GL_UnloadLibrary_f;

        public static void SDL_GL_UnloadLibrary() => SDL_GL_UnloadLibrary_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int SDL_EventFilter(void* userdata, SDL_Event* evt);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_AddEventWatch_d(SDL_EventFilter filter, void* userdata);

        private static SDL_AddEventWatch_d SDL_AddEventWatch_f;

        public static void SDL_AddEventWatch(SDL_EventFilter filter, void* userdata) =>
            SDL_AddEventWatch_f(filter, userdata);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_DelEventWatch_d(SDL_EventFilter filter, void* userdata);

        private static SDL_DelEventWatch_d SDL_DelEventWatch_f;

        public static void SDL_DelEventWatch(SDL_EventFilter filter, void* userdata) =>
            SDL_DelEventWatch_f(filter, userdata);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte SDL_EventState_d(SDL_EventType type, int state);

        private static SDL_EventState_d SDL_EventState_f;

        public static byte SDL_EventState(SDL_EventType type, int state) => SDL_EventState_f(type, state);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_FilterEvents_d(SDL_EventFilter filter, void* userdata);

        private static SDL_FilterEvents_d SDL_FilterEvents_f;

        public static void SDL_FilterEvents(SDL_EventFilter filter, void* userdata) =>
            SDL_FilterEvents_f(filter, userdata);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_FlushEvent_d(SDL_EventType type);

        private static SDL_FlushEvent_d SDL_FlushEvent_f;

        public static void SDL_FlushEvent(SDL_EventType type) => SDL_FlushEvent_f(type);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_FlushEvents_d(SDL_EventType minType, SDL_EventType maxType);

        private static SDL_FlushEvents_d SDL_FlushEvents_f;

        public static void SDL_FlushEvents(SDL_EventType minType, SDL_EventType maxType) =>
            SDL_FlushEvents_f(minType, maxType);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_GetEventFilter_d(IntPtr* filter, void** userdata);

        private static SDL_GetEventFilter_d SDL_GetEventFilter_f;

        public static bool SDL_GetEventFilter(IntPtr* filter, void** userdata) =>
            SDL_GetEventFilter_f(filter, userdata);

        public static bool SDL_GetEventFilter(out SDL_EventFilter filter, void** userdata)
        {
            IntPtr ptr;
            bool result = SDL_GetEventFilter(&ptr, userdata);
            if (result)
                filter = Marshal.GetDelegateForFunctionPointer<SDL_EventFilter>(ptr);
            else
                filter = null;
            return result;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_HasEvent_d(SDL_EventType type);

        private static SDL_HasEvent_d SDL_HasEvent_f;

        public static bool SDL_HasEvent(SDL_EventType type) => SDL_HasEvent_f(type);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_HasEvents_d(SDL_EventType minType, SDL_EventType maxType);

        private static SDL_HasEvents_d SDL_HasEvents_f;

        public static bool SDL_HasEvents(SDL_EventType minType, SDL_EventType maxType) =>
            SDL_HasEvents_f(minType, maxType);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_PeepEvents_d(SDL_Event* events, int numevents, SDL_eventaction action,
            SDL_EventType minType, SDL_EventType maxType);

        private static SDL_PeepEvents_d SDL_PeepEvents_f;

        public static int SDL_PeepEvents(SDL_Event* events, int numevents, SDL_eventaction action,
            SDL_EventType minType, SDL_EventType maxType) =>
            SDL_PeepEvents_f(events, numevents, action, minType, maxType);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_PollEvent_d(SDL_Event* evt);

        private static SDL_PollEvent_d SDL_PollEvent_f;

        public static int SDL_PollEvent(SDL_Event* evt) => SDL_PollEvent_f(evt);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_PumpEvents_d();

        private static SDL_PumpEvents_d SDL_PumpEvents_f;

        public static void SDL_PumpEvents() => SDL_PumpEvents_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_PushEvent_d(SDL_Event* evt);

        private static SDL_PushEvent_d SDL_PushEvent_f;

        public static int SDL_PushEvent(SDL_Event* evt) => SDL_PushEvent_f(evt);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint SDL_RegisterEvents_d(int numevents);

        private static SDL_RegisterEvents_d SDL_RegisterEvents_f;

        public static uint SDL_RegisterEvents(int numevents) => SDL_RegisterEvents_f(numevents);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetEventFilter_d(SDL_EventFilter filter, void* userdata);

        private static SDL_SetEventFilter_d SDL_SetEventFilter_f;

        public static void SDL_SetEventFilter(SDL_EventFilter filter, void* userdata) =>
            SDL_SetEventFilter_f(filter, userdata);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_WaitEvent_d(SDL_Event* evt);

        private static SDL_WaitEvent_d SDL_WaitEvent_f;

        public static int SDL_WaitEvent(SDL_Event* evt) => SDL_WaitEvent_f(evt);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_WaitEventTimeout_d(SDL_Event* evt, int timeout);

        private static SDL_WaitEventTimeout_d SDL_WaitEventTimeout_f;

        public static int SDL_WaitEventTimeout(SDL_Event* evt, int timeout) => SDL_WaitEventTimeout_f(evt, timeout);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Keycode SDL_GetKeyFromName_d(string name);

        private static SDL_GetKeyFromName_d SDL_GetKeyFromName_f;

        public static SDL_Keycode SDL_GetKeyFromName(string name) => SDL_GetKeyFromName_f(name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Keycode SDL_GetKeyFromScancode_d(SDL_Scancode scancode);

        private static SDL_GetKeyFromScancode_d SDL_GetKeyFromScancode_f;

        public static SDL_Keycode SDL_GetKeyFromScancode(SDL_Scancode scancode) => SDL_GetKeyFromScancode_f(scancode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetKeyName_d(SDL_Keycode key);

        private static SDL_GetKeyName_d SDL_GetKeyName_f;

        public static byte* SDL_GetKeyName(SDL_Keycode key) => SDL_GetKeyName_f(key);

        public static string SDL_GetKeyNameString(SDL_Keycode key)
        {
            return GetString(SDL_GetKeyName(key));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Window SDL_GetKeyboardFocus_d();

        private static SDL_GetKeyboardFocus_d SDL_GetKeyboardFocus_f;

        public static SDL_Window SDL_GetKeyboardFocus() => SDL_GetKeyboardFocus_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetKeyboardState_d(int* numKeys);

        private static SDL_GetKeyboardState_d SDL_GetKeyboardState_f;

        public static byte* SDL_GetKeyboardState(int* numKeys) => SDL_GetKeyboardState_f(numKeys);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Keymod SDL_GetModState_d();

        private static SDL_GetModState_d SDL_GetModState_f;

        public static SDL_Keymod SDL_GetModState() => SDL_GetModState_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Scancode SDL_GetScancodeFromKey_d(SDL_Keycode key);

        private static SDL_GetScancodeFromKey_d SDL_GetScancodeFromKey_f;

        public static SDL_Scancode SDL_GetScancodeFromKey(SDL_Keycode key) => SDL_GetScancodeFromKey_f(key);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Scancode SDL_GetScancodeFromName_d(string name);

        private static SDL_GetScancodeFromName_d SDL_GetScancodeFromName_f;

        public static SDL_Scancode SDL_GetScancodeFromName(string name) => SDL_GetScancodeFromName_f(name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetScancodeName_d(SDL_Scancode scancode);

        private static SDL_GetScancodeName_d SDL_GetScancodeName_f;

        public static byte* SDL_GetScancodeName(SDL_Scancode scancode) => SDL_GetScancodeName_f(scancode);

        public static string SDL_GetScancodeNameString(SDL_Scancode scancode)
        {
            return GetString(SDL_GetScancodeName(scancode));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_HasScreenKeyboardSupport_d();

        private static SDL_HasScreenKeyboardSupport_d SDL_HasScreenKeyboardSupport_f;

        public static bool SDL_HasScreenKeyboardSupport() => SDL_HasScreenKeyboardSupport_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_IsScreenKeyboardShown_d(SDL_Window window);

        private static SDL_IsScreenKeyboardShown_d SDL_IsScreenKeyboardShown_f;

        public static bool SDL_IsScreenKeyboardShown(SDL_Window window) => SDL_IsScreenKeyboardShown_f(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_IsTextInputActive_d();

        private static SDL_IsTextInputActive_d SDL_IsTextInputActive_f;

        public static bool SDL_IsTextInputActive() => SDL_IsTextInputActive_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetModState_d(SDL_Keymod modstate);

        private static SDL_SetModState_d SDL_SetModState_f;

        public static void SDL_SetModState(SDL_Keymod modstate) => SDL_SetModState_f(modstate);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetTextInputRect_d(SDL_Rect* rect);

        private static SDL_SetTextInputRect_d SDL_SetTextInputRect_f;

        public static void SDL_SetTextInputRect(SDL_Rect* rect) => SDL_SetTextInputRect_f(rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_StartTextInput_d();

        private static SDL_StartTextInput_d SDL_StartTextInput_f;

        public static void SDL_StartTextInput() => SDL_StartTextInput_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_StopTextInput_d();

        private static SDL_StopTextInput_d SDL_StopTextInput_f;

        public static void SDL_StopTextInput() => SDL_StopTextInput_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_CaptureMouse_d(bool enabled);

        private static SDL_CaptureMouse_d SDL_CaptureMouse_f;

        public static int SDL_CaptureMouse(bool enabled) => SDL_CaptureMouse_f(enabled);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Cursor SDL_CreateCursor_d(byte* data, byte* mask, int w, int h, int hotX, int hotY);

        private static SDL_CreateCursor_d SDL_CreateCursor_f;

        public static SDL_Cursor SDL_CreateCursor(byte* data, byte* mask, int w, int h, int hotX, int hotY) =>
            SDL_CreateCursor_f(data, mask, w, h, hotX, hotY);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Cursor SDL_CreateSystemCursor_d(SDL_SystemCursor id);

        private static SDL_CreateSystemCursor_d SDL_CreateSystemCursor_f;

        public static SDL_Cursor SDL_CreateSystemCursor(SDL_SystemCursor id) => SDL_CreateSystemCursor_f(id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_FreeCursor_d(SDL_Cursor cursor);

        private static SDL_FreeCursor_d SDL_FreeCursor_f;

        public static void SDL_FreeCursor(SDL_Cursor cursor) => SDL_FreeCursor_f(cursor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Cursor SDL_GetCursor_d();

        private static SDL_GetCursor_d SDL_GetCursor_f;

        public static SDL_Cursor SDL_GetCursor() => SDL_GetCursor_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Cursor SDL_GetDefaultCursor_d();

        private static SDL_GetDefaultCursor_d SDL_GetDefaultCursor_f;

        public static SDL_Cursor SDL_GetDefaultCursor() => SDL_GetDefaultCursor_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_ButtonMask SDL_GetGlobalMouseState_d(int* x, int* y);

        private static SDL_GetGlobalMouseState_d SDL_GetGlobalMouseState_f;

        public static SDL_ButtonMask SDL_GetGlobalMouseState(int* x, int* y) => SDL_GetGlobalMouseState_f(x, y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Window SDL_GetMouseFocus_d();

        private static SDL_GetMouseFocus_d SDL_GetMouseFocus_f;

        public static SDL_Window SDL_GetMouseFocus() => SDL_GetMouseFocus_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_ButtonMask SDL_GetMouseState_d(int* x, int* y);

        private static SDL_GetMouseState_d SDL_GetMouseState_f;

        public static SDL_ButtonMask SDL_GetMouseState(int* x, int* y) => SDL_GetMouseState_f(x, y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_GetRelativeMouseMode_d();

        private static SDL_GetRelativeMouseMode_d SDL_GetRelativeMouseMode_f;

        public static bool SDL_GetRelativeMouseMode() => SDL_GetRelativeMouseMode_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_ButtonMask SDL_GetRelativeMouseState_d(int* x, int* y);

        private static SDL_GetRelativeMouseState_d SDL_GetRelativeMouseState_f;

        public static SDL_ButtonMask SDL_GetRelativeMouseState(int* x, int* y) => SDL_GetRelativeMouseState_f(x, y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetCursor_d(SDL_Cursor cursor);

        private static SDL_SetCursor_d SDL_SetCursor_f;

        public static void SDL_SetCursor(SDL_Cursor cursor) => SDL_SetCursor_f(cursor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetRelativeMouseMode_d(bool enabled);

        private static SDL_SetRelativeMouseMode_d SDL_SetRelativeMouseMode_f;

        public static int SDL_SetRelativeMouseMode(bool enabled) => SDL_SetRelativeMouseMode_f(enabled);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_ShowCursor_d(int toggle);

        private static SDL_ShowCursor_d SDL_ShowCursor_f;

        public static int SDL_ShowCursor(int toggle) => SDL_ShowCursor_f(toggle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_WarpMouseGlobal_d(int x, int y);

        private static SDL_WarpMouseGlobal_d SDL_WarpMouseGlobal_f;

        public static int SDL_WarpMouseGlobal(int x, int y) => SDL_WarpMouseGlobal_f(x, y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_WarpMouseInWindow_d(SDL_Window window, int x, int y);

        private static SDL_WarpMouseInWindow_d SDL_WarpMouseInWindow_f;

        public static void SDL_WarpMouseInWindow(SDL_Window window, int x, int y) =>
            SDL_WarpMouseInWindow_f(window, x, y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetNumTouchDevices_d();

        private static SDL_GetNumTouchDevices_d SDL_GetNumTouchDevices_f;

        public static int SDL_GetNumTouchDevices() => SDL_GetNumTouchDevices_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_TouchID SDL_GetTouchDevice_d(int index);

        private static SDL_GetTouchDevice_d SDL_GetTouchDevice_f;

        public static SDL_TouchID SDL_GetTouchDevice(int index) => SDL_GetTouchDevice_f(index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetNumTouchFingers_d(SDL_TouchID touchID);

        private static SDL_GetNumTouchFingers_d SDL_GetNumTouchFingers_f;

        public static int SDL_GetNumTouchFingers(SDL_TouchID touchID) => SDL_GetNumTouchFingers_f(touchID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Finger SDL_GetTouchFinger_d(SDL_TouchID touchID, int index);

        private static SDL_GetTouchFinger_d SDL_GetTouchFinger_f;

        public static SDL_Finger SDL_GetTouchFinger(SDL_TouchID touchID, int index) =>
            SDL_GetTouchFinger_f(touchID, index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_JoystickClose_d(SDL_Joystick joystick);

        private static SDL_JoystickClose_d SDL_JoystickClose_f;

        public static void SDL_JoystickClose(SDL_Joystick joystick) => SDL_JoystickClose_f(joystick);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_JoystickPowerLevel SDL_JoystickCurrentPowerLevel_d(SDL_Joystick joystick);

        private static SDL_JoystickCurrentPowerLevel_d SDL_JoystickCurrentPowerLevel_f;

        public static SDL_JoystickPowerLevel SDL_JoystickCurrentPowerLevel(SDL_Joystick joystick) =>
            SDL_JoystickCurrentPowerLevel_f(joystick);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Joystick SDL_JoystickFromInstanceID_d(SDL_JoystickID joyid);

        private static SDL_JoystickFromInstanceID_d SDL_JoystickFromInstanceID_f;

        public static SDL_Joystick SDL_JoystickFromInstanceID(SDL_JoystickID joyid) =>
            SDL_JoystickFromInstanceID_f(joyid);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_JoystickGetAttached_d(SDL_Joystick joystick);

        private static SDL_JoystickGetAttached_d SDL_JoystickGetAttached_f;

        public static bool SDL_JoystickGetAttached(SDL_Joystick joystick) => SDL_JoystickGetAttached_f(joystick);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate short SDL_JoystickGetAxis_d(SDL_Joystick joystick, int axis);

        private static SDL_JoystickGetAxis_d SDL_JoystickGetAxis_f;

        public static short SDL_JoystickGetAxis(SDL_Joystick joystick, int axis) =>
            SDL_JoystickGetAxis_f(joystick, axis);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_JoystickGetBall_d(SDL_Joystick joystick, int ball, int* dx, int* dy);

        private static SDL_JoystickGetBall_d SDL_JoystickGetBall_f;

        public static int SDL_JoystickGetBall(SDL_Joystick joystick, int ball, int* dx, int* dy) =>
            SDL_JoystickGetBall_f(joystick, ball, dx, dy);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte SDL_JoystickGetButton_d(SDL_Joystick joystick, int button);

        private static SDL_JoystickGetButton_d SDL_JoystickGetButton_f;

        public static byte SDL_JoystickGetButton(SDL_Joystick joystick, int button) =>
            SDL_JoystickGetButton_f(joystick, button);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate Guid SDL_JoystickGetDeviceGUID_d(int deviceIndex);

        private static SDL_JoystickGetDeviceGUID_d SDL_JoystickGetDeviceGUID_f;

        public static Guid SDL_JoystickGetDeviceGUID(int deviceIndex) => SDL_JoystickGetDeviceGUID_f(deviceIndex);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate Guid SDL_JoystickGetGUID_d(SDL_Joystick joystick);

        private static SDL_JoystickGetGUID_d SDL_JoystickGetGUID_f;

        public static Guid SDL_JoystickGetGUID(SDL_Joystick joystick) => SDL_JoystickGetGUID_f(joystick);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate Guid SDL_JoystickGetGUIDFromString_d(string pchGUID);

        private static SDL_JoystickGetGUIDFromString_d SDL_JoystickGetGUIDFromString_f;

        public static Guid SDL_JoystickGetGUIDFromString(string pchGUID) => SDL_JoystickGetGUIDFromString_f(pchGUID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_JoystickGetGUIDString_d(Guid guid, byte* pszGUID, int cbGUID);

        private static SDL_JoystickGetGUIDString_d SDL_JoystickGetGUIDString_f;

        public static void SDL_JoystickGetGUIDString(Guid guid, byte* pszGUID, int cbGUID) =>
            SDL_JoystickGetGUIDString_f(guid, pszGUID, cbGUID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Hat SDL_JoystickGetHat_d(SDL_Joystick joystick, int hat);

        private static SDL_JoystickGetHat_d SDL_JoystickGetHat_f;

        public static SDL_Hat SDL_JoystickGetHat(SDL_Joystick joystick, int hat) => SDL_JoystickGetHat_f(joystick, hat);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_JoystickID SDL_JoystickInstanceID_d(SDL_Joystick joystick);

        private static SDL_JoystickInstanceID_d SDL_JoystickInstanceID_f;

        public static SDL_JoystickID SDL_JoystickInstanceID(SDL_Joystick joystick) =>
            SDL_JoystickInstanceID_f(joystick);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_JoystickName_d(SDL_Joystick joystick);

        private static SDL_JoystickName_d SDL_JoystickName_f;

        public static byte* SDL_JoystickName(SDL_Joystick joystick) => SDL_JoystickName_f(joystick);

        public static string SDL_JoystickNameString(SDL_Joystick joystick)
        {
            return GetString(SDL_JoystickName(joystick));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_JoystickNameForIndex_d(int deviceIndex);

        private static SDL_JoystickNameForIndex_d SDL_JoystickNameForIndex_f;

        public static byte* SDL_JoystickNameForIndex(int deviceIndex) => SDL_JoystickNameForIndex_f(deviceIndex);

        public static string SDL_JoystickNameForIndexString(int deviceIndex)
        {
            return GetString(SDL_JoystickNameForIndex(deviceIndex));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_JoystickNumAxes_d(SDL_Joystick joystick);

        private static SDL_JoystickNumAxes_d SDL_JoystickNumAxes_f;

        public static int SDL_JoystickNumAxes(SDL_Joystick joystick) => SDL_JoystickNumAxes_f(joystick);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_JoystickNumBalls_d(SDL_Joystick joystick);

        private static SDL_JoystickNumBalls_d SDL_JoystickNumBalls_f;

        public static int SDL_JoystickNumBalls(SDL_Joystick joystick) => SDL_JoystickNumBalls_f(joystick);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_JoystickNumButtons_d(SDL_Joystick joystick);

        private static SDL_JoystickNumButtons_d SDL_JoystickNumButtons_f;

        public static int SDL_JoystickNumButtons(SDL_Joystick joystick) => SDL_JoystickNumButtons_f(joystick);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_JoystickNumHats_d(SDL_Joystick joystick);

        private static SDL_JoystickNumHats_d SDL_JoystickNumHats_f;

        public static int SDL_JoystickNumHats(SDL_Joystick joystick) => SDL_JoystickNumHats_f(joystick);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Joystick SDL_JoystickOpen_d(int deviceIndex);

        private static SDL_JoystickOpen_d SDL_JoystickOpen_f;

        public static SDL_Joystick SDL_JoystickOpen(int deviceIndex) => SDL_JoystickOpen_f(deviceIndex);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_JoystickUpdate_d();

        private static SDL_JoystickUpdate_d SDL_JoystickUpdate_f;

        public static void SDL_JoystickUpdate() => SDL_JoystickUpdate_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_NumJoysticks_d();

        private static SDL_NumJoysticks_d SDL_NumJoysticks_f;

        public static int SDL_NumJoysticks() => SDL_NumJoysticks_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GameControllerAddMapping_d(string mappingString);

        private static SDL_GameControllerAddMapping_d SDL_GameControllerAddMapping_f;

        public static int SDL_GameControllerAddMapping(string mappingString) =>
            SDL_GameControllerAddMapping_f(mappingString);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GameControllerClose_d(SDL_GameController gamecontroller);

        private static SDL_GameControllerClose_d SDL_GameControllerClose_f;

        public static int SDL_GameControllerClose(SDL_GameController gamecontroller) =>
            SDL_GameControllerClose_f(gamecontroller);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_GameController SDL_GameControllerFromInstanceID_d(SDL_JoystickID joyid);

        private static SDL_GameControllerFromInstanceID_d SDL_GameControllerFromInstanceID_f;

        public static SDL_GameController SDL_GameControllerFromInstanceID(SDL_JoystickID joyid) =>
            SDL_GameControllerFromInstanceID_f(joyid);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_GameControllerGetAttached_d(SDL_GameController gamecontroller);

        private static SDL_GameControllerGetAttached_d SDL_GameControllerGetAttached_f;

        public static bool SDL_GameControllerGetAttached(SDL_GameController gamecontroller) =>
            SDL_GameControllerGetAttached_f(gamecontroller);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate short SDL_GameControllerGetAxis_d(SDL_GameController gameController,
            SDL_GameControllerAxis axis);

        private static SDL_GameControllerGetAxis_d SDL_GameControllerGetAxis_f;

        public static short SDL_GameControllerGetAxis(SDL_GameController gameController, SDL_GameControllerAxis axis) =>
            SDL_GameControllerGetAxis_f(gameController, axis);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_GameControllerAxis SDL_GameControllerGetAxisFromString_d(string pchString);

        private static SDL_GameControllerGetAxisFromString_d SDL_GameControllerGetAxisFromString_f;

        public static SDL_GameControllerAxis SDL_GameControllerGetAxisFromString(string pchString) =>
            SDL_GameControllerGetAxisFromString_f(pchString);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_GameControllerButtonBind SDL_GameControllerGetBindForAxis_d(
            SDL_GameController gameController, SDL_GameControllerAxis axis);

        private static SDL_GameControllerGetBindForAxis_d SDL_GameControllerGetBindForAxis_f;

        public static SDL_GameControllerButtonBind SDL_GameControllerGetBindForAxis(SDL_GameController gameController,
            SDL_GameControllerAxis axis) => SDL_GameControllerGetBindForAxis_f(gameController, axis);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_GameControllerButtonBind SDL_GameControllerGetBindForButton_d(
            SDL_GameController gameController, SDL_GameControllerButton buttons);

        private static SDL_GameControllerGetBindForButton_d SDL_GameControllerGetBindForButton_f;

        public static SDL_GameControllerButtonBind SDL_GameControllerGetBindForButton(SDL_GameController gameController,
            SDL_GameControllerButton buttons) => SDL_GameControllerGetBindForButton_f(gameController, buttons);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte SDL_GameControllerGetButton_d(SDL_GameController gameController,
            SDL_GameControllerButton button);

        private static SDL_GameControllerGetButton_d SDL_GameControllerGetButton_f;

        public static byte SDL_GameControllerGetButton(SDL_GameController gameController,
            SDL_GameControllerButton button) => SDL_GameControllerGetButton_f(gameController, button);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_GameControllerButton SDL_GameControllerGetButtonFromString_d(string pchString);

        private static SDL_GameControllerGetButtonFromString_d SDL_GameControllerGetButtonFromString_f;

        public static SDL_GameControllerButton SDL_GameControllerGetButtonFromString(string pchString) =>
            SDL_GameControllerGetButtonFromString_f(pchString);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Joystick SDL_GameControllerGetJoystick_d(SDL_GameController gameController);

        private static SDL_GameControllerGetJoystick_d SDL_GameControllerGetJoystick_f;

        public static SDL_Joystick SDL_GameControllerGetJoystick(SDL_GameController gameController) =>
            SDL_GameControllerGetJoystick_f(gameController);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GameControllerGetStringForAxis_d(SDL_GameControllerAxis axis);

        private static SDL_GameControllerGetStringForAxis_d SDL_GameControllerGetStringForAxis_f;

        public static byte* SDL_GameControllerGetStringForAxis(SDL_GameControllerAxis axis) =>
            SDL_GameControllerGetStringForAxis_f(axis);

        public static string SDL_GameControllerGetStringForAxisString(SDL_GameControllerAxis axis)
        {
            return GetString(SDL_GameControllerGetStringForAxis(axis));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GameControllerGetStringForButton_d(SDL_GameControllerButton button);

        private static SDL_GameControllerGetStringForButton_d SDL_GameControllerGetStringForButton_f;

        public static byte* SDL_GameControllerGetStringForButton(SDL_GameControllerButton button) =>
            SDL_GameControllerGetStringForButton_f(button);

        public static string SDL_GameControllerGetStringForButtonString(SDL_GameControllerButton button)
        {
            return GetString(SDL_GameControllerGetStringForButton(button));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GameControllerMapping_d(SDL_GameController gameController);

        private static SDL_GameControllerMapping_d SDL_GameControllerMapping_f;

        public static byte* SDL_GameControllerMapping(SDL_GameController gameController) =>
            SDL_GameControllerMapping_f(gameController);

        public static string SDL_GameControllerMappingString(SDL_GameController gameController)
        {
            return GetString(SDL_GameControllerMapping(gameController));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GameControllerMappingForGUID_d(Guid guid);

        private static SDL_GameControllerMappingForGUID_d SDL_GameControllerMappingForGUID_f;

        public static byte* SDL_GameControllerMappingForGUID(Guid guid) => SDL_GameControllerMappingForGUID_f(guid);

        public static string SDL_GameControllerMappingForGUIDString(Guid guid)
        {
            return GetString(SDL_GameControllerMappingForGUID(guid));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GameControllerName_d(SDL_GameController gameController);

        private static SDL_GameControllerName_d SDL_GameControllerName_f;

        public static byte* SDL_GameControllerName(SDL_GameController gameController) =>
            SDL_GameControllerName_f(gameController);

        public static string SDL_GameControllerNameString(SDL_GameController gameController)
        {
            return GetString(SDL_GameControllerName(gameController));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GameControllerNameForIndex_d(int joystickIndex);

        private static SDL_GameControllerNameForIndex_d SDL_GameControllerNameForIndex_f;

        public static byte* SDL_GameControllerNameForIndex(int joystickIndex) =>
            SDL_GameControllerNameForIndex_f(joystickIndex);

        public static string SDL_GameControllerNameForIndexString(int joystickIndex)
        {
            return GetString(SDL_GameControllerNameForIndex(joystickIndex));
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_GameController SDL_GameControllerOpen_d(int joystickIndex);

        private static SDL_GameControllerOpen_d SDL_GameControllerOpen_f;

        public static SDL_GameController SDL_GameControllerOpen(int joystickIndex) =>
            SDL_GameControllerOpen_f(joystickIndex);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GameControllerUpdate_d();

        private static SDL_GameControllerUpdate_d SDL_GameControllerUpdate_f;

        public static void SDL_GameControllerUpdate() => SDL_GameControllerUpdate_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_IsGameController_d(int joystickIndex);

        private static SDL_IsGameController_d SDL_IsGameController_f;

        public static bool SDL_IsGameController(int joystickIndex) => SDL_IsGameController_f(joystickIndex);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetClipboardText_d();

        private static SDL_GetClipboardText_d SDL_GetClipboardText_f;

        public static byte* SDL_GetClipboardText() => SDL_GetClipboardText_f();

        public static string SDL_GetClipboardTextString()
        {
            return GetString(SDL_GetClipboardText());
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_HasClipboardText_d();

        private static SDL_HasClipboardText_d SDL_HasClipboardText_f;

        public static bool SDL_HasClipboardText() => SDL_HasClipboardText_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetClipboardText_d(string text);

        private static SDL_SetClipboardText_d SDL_SetClipboardText_f;

        public static int SDL_SetClipboardText(string text) => SDL_SetClipboardText_f(text);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetError_d();

        private static SDL_GetError_d SDL_GetError_f;

        public static byte* _SDL_GetError() => SDL_GetError_f();

        public static string SDL_GetError()
        {
            return GetString(_SDL_GetError());
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_ClearError_d();

        private static SDL_ClearError_d SDL_ClearError_f;

        public static void SDL_ClearError() => SDL_ClearError_f();

        private static string GetString(byte* ptr)
        {
            if (ptr == null)
            {
                return null;
            }

            int count = 0;
            while (*(ptr + count) != 0)
            {
                count += 1;
            }

            return Encoding.UTF8.GetString(ptr, count);
        }
    }
}