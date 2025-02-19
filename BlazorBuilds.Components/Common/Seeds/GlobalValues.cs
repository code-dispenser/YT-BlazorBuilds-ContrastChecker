namespace BlazorBuilds.Components.Common.Seeds;

public class GlobalValues
{
    public const string Incorrect_Hex_Value_Exception_Message = "Invalid hex value. The value should only contain a-f, A-F and 0-9 characters, optionally starting with the pound # symbol";
    public const string Invalid_Colour_Entry_Message          = "Invalid hex colour value. Please use the full format of a pound sign followed by six characters, each being A to F or 0 to 9";
    public const string Aria_MaxLength_Reached_Message        = "That key was not used as the maximum length of the input has been reached.";
    public const string Aria_Format_Contrast_Ratio_Message    = "Contrast ratio {0} to 1";  
    public const string Regex_Hex_Colour_Pattern              = "^#?[a-fA-F0-9]{6}$";
    public const string Regex_Colour_Key_Pattern              = "^[#a-fA-F0-9]$";
    public const string Regex_Hex_Replace_Pattern             = "[^a-fA-F0-9]";
    public const string Icon_Pass_Modifier_Class              = "contrast-checker__icon--pass";
    public const string Icon_Fail_Modifier_Class              = "contrast-checker__icon--fail";
    public const string Contrast_Ratio_Pass_Text              = "Passed";
    public const string Contrast_Ratio_Fail_Text              = "Failed";

    public const double Min_AAA_Normal_Text_Ratio = 7;
    public const double Min_AAA_Large_Text_Ratio  = 4.5;
    public const double Min_AA_Normal_Text_Ratio  = 4.5;
    public const double Min_AA_Large_Text_Ratio   = 3;
    public const double Min_Borders_Ratio         = 3;
    public const double Regular_Font_PX           = 16;
    public const double Large_Font_Bold_PX        = 18.66;
    public const double Large_Font_PX             = 24;

    public const string Style_As_Dark  = "dark";
    public const string Style_As_Light = "light";

    public static string? GetStyleAsValue(StyleAs styleAs)

        => styleAs switch
        {
            StyleAs.OnLight => Style_As_Light,
            StyleAs.OnDark => Style_As_Dark,
            _ => null
        };

}

