using BlazorBuilds.Components.Common.Seeds;
using BlazorBuilds.Components.Common.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorBuilds.Components.ContrastChecker;

public partial class ContrastChecker
{
    [Parameter] public string HexColourValueOne  { get; set; } = "#000000";
    [Parameter] public string HexColourValueTwo  { get; set; } = "#FFFFFF";
    [Parameter] public double AAALargeTextRatio  { get; set; } = GlobalValues.Min_AAA_Large_Text_Ratio;
    [Parameter] public double AALargeTextRatio   { get; set; } = GlobalValues.Min_AA_Large_Text_Ratio;
    [Parameter] public double AAANormalTextRatio { get; set; } = GlobalValues.Min_AAA_Normal_Text_Ratio;
    [Parameter] public double AANormalTextRatio  { get; set; } = GlobalValues.Min_AA_Normal_Text_Ratio;
    [Parameter] public double Regular_Font_PX    { get; set; } = GlobalValues.Regular_Font_PX;
    [Parameter] public double Large_Font_Bold_PX { get; set; } = GlobalValues.Large_Font_Bold_PX;
    [Parameter] public double Large_Font_PX      { get; set; } = GlobalValues.Large_Font_PX;
    [Parameter] public StyleAs StyleAs           { get; set; } = StyleAs.Dynamic;

    private string _ariaAlertMessage    = String.Empty;
    private string _colourOneID         = Guid.NewGuid().ToString();
    private string _colourTwoID         = Guid.NewGuid().ToString();
    private string _hexColourOne        = "#000000";
    private string _hexColourTwo        = "#FFFFFF";
    private string _displayTextColour   = "#000000";
    private string _displayBgColour     = "#FFFFFF";
    private bool   _colourOneIsValid    = true;
    private bool   _colourTwoIsValid    = true;
    private string _errorMessageID      = Guid.NewGuid().ToString();
    private double _contrastRatio = 0;

    protected override void OnParametersSet()
    {
        _hexColourOne  = (true == ColourUtils.IsHexValueValid(HexColourValueOne)) ? HexColourValueOne : throw new ArgumentException(GlobalValues.Incorrect_Hex_Value_Exception_Message);
        _hexColourTwo  = (true == ColourUtils.IsHexValueValid(HexColourValueTwo)) ? HexColourValueTwo : throw new ArgumentException(GlobalValues.Incorrect_Hex_Value_Exception_Message);
        _contrastRatio = GetContrastRatio(_hexColourOne, _hexColourTwo);
    }

    private void SwapColours()
    {
        (_colourOneIsValid, _colourTwoIsValid) = (_colourTwoIsValid, _colourOneIsValid);
        (_displayTextColour, _displayBgColour) = (_hexColourOne, _hexColourTwo) = (_hexColourTwo, _hexColourOne);
    }

    private void ColourOneOnInputHandler(ChangeEventArgs args)
    {
        _colourOneIsValid   = true;
        _displayTextColour  =  _hexColourOne = args.Value!.ToString()!;
        _contrastRatio      = GetContrastRatio(_hexColourOne, _hexColourTwo);
    }

    private void ColourTwoOnInputHandler(ChangeEventArgs args)
    {
        _colourTwoIsValid = true;
        _displayBgColour  = _hexColourTwo   = args.Value!.ToString()!;
        _contrastRatio    = GetContrastRatio(_hexColourOne, _hexColourTwo);
    }
    
    private async Task OnColourOneInput(string value)
    {
        _hexColourOne = String.Concat("#", ColourUtils.ReplaceNonHexChars(value));

        if (true == ColourUtils.IsHexValueValid(_hexColourOne)) _displayTextColour  = _hexColourOne;

        _colourOneIsValid = _displayTextColour == _hexColourOne;
        _contrastRatio    = GetContrastRatio(_hexColourOne, _hexColourTwo);

        if (_contrastRatio > 0) SetAriaAlert(String.Format(GlobalValues.Aria_Format_Contrast_Ratio_Message, _contrastRatio));

        await Task.CompletedTask;
    }

    private async Task OnColourTwoInput(string value)
    {
        _hexColourTwo = String.Concat("#", ColourUtils.ReplaceNonHexChars(value));

        if (true == ColourUtils.IsHexValueValid(_hexColourTwo)) _displayBgColour = _hexColourTwo;

        _colourTwoIsValid = _displayBgColour == _hexColourTwo;
        _contrastRatio    = GetContrastRatio(_hexColourOne, _hexColourTwo);

        if (_contrastRatio > 0) SetAriaAlert(String.Format(GlobalValues.Aria_Format_Contrast_Ratio_Message, _contrastRatio));

        await Task.CompletedTask;
    }

    private double GetContrastRatio(string hexColourOne, string hexColourTwo)

        => (ColourUtils.IsHexValueValid(hexColourOne) && ColourUtils.IsHexValueValid(hexColourTwo))
                ? ColourUtils.CalculateContrast(ColourUtils.HexToRgbColour(hexColourOne), ColourUtils.HexToRgbColour(hexColourTwo))
                : 0;

    private string GetIconResultClass(double requiredRatio)

        => requiredRatio <= _contrastRatio ? GlobalValues.Icon_Pass_Modifier_Class : GlobalValues.Icon_Fail_Modifier_Class;

    private string GetPassFailTextForValue(double requiredRatio)

        => requiredRatio <= _contrastRatio ? GlobalValues.Contrast_Ratio_Pass_Text : GlobalValues.Contrast_Ratio_Fail_Text;
    
    private void HandleColourOneMaxlength(KeyboardEventArgs keyArgs)
    {
        ClearAriaAlert();

        if (_hexColourOne.Length == 7 && ColourUtils.IsValidKeyChar(keyArgs.Key)) SetAriaAlert(GlobalValues.Aria_MaxLength_Reached_Message);
    }

    private void HandleColourTwoMaxlength(KeyboardEventArgs keyArgs)
    {
        ClearAriaAlert();

        if (_hexColourTwo.Length == 7 && ColourUtils.IsValidKeyChar(keyArgs.Key)) SetAriaAlert(GlobalValues.Aria_MaxLength_Reached_Message);
    }

    private void ClearAriaAlert()

        => _ariaAlertMessage = String.Empty;

    private void SetAriaAlert(string message)

        => _ariaAlertMessage = message;
}
