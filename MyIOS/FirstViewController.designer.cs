// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MyIOS
{
    [Register ("FirstViewController")]
    partial class FirstViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel MessageLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton startService { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton stopService { get; set; }

        [Action ("StartService_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void StartService_TouchUpInside (UIKit.UIButton sender);

        [Action ("StopService_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void StopService_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton487_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton487_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton737_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton737_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (MessageLabel != null) {
                MessageLabel.Dispose ();
                MessageLabel = null;
            }

            if (startService != null) {
                startService.Dispose ();
                startService = null;
            }

            if (stopService != null) {
                stopService.Dispose ();
                stopService = null;
            }
        }
    }
}