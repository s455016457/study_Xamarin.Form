using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Services
{
    public class ListMenuDataStore
    {
        public Models.MenuItem GetMenuItem()
        {
            Models.MenuItem baseMenuItem = new Models.MenuItem("caidan", "菜单", ImageSource.FromFile("setting.png"));
            IList<Models.MenuItem> list = new List<Models.MenuItem>();
            baseMenuItem.ChildrenMenu = list;
            list.Add(new Models.MenuItem("HomePage", "Home", ImageSource.FromFile("setting.png")));
            list.Add(new Models.MenuItem("ListMenuPage", "菜单", ImageSource.FromFile("setting.png")));
            list.Add(new Models.MenuItem("ListMenuAnimatePage", "有滑动效果菜单", ImageSource.FromFile("setting.png")));
            list.Add(new Models.MenuItem("ListMenuAnimatePage2", "有滑动效果菜单2", ImageSource.FromFile("setting.png")));
            list.Add(new Models.MenuItem("ClassAndSubclassesPage", "类继承结构", ImageSource.FromFile("setting.png")));

            Models.MenuItem controlMenuItem = new Models.MenuItem("ControlGroup", "控件", "setting.png");
            controlMenuItem.ParentMenuItem = baseMenuItem;
            controlMenuItem.ChildrenMenu = new List<Models.MenuItem>();
            list.Add(controlMenuItem);

            controlMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestLabelPage", "Label", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = controlMenuItem
            });

            controlMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestColorPage", "Color", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = controlMenuItem
            });

            controlMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestEntryPage", "Entry", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = controlMenuItem
            });

            controlMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestEditorPage", "Editor", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = controlMenuItem
            });

            controlMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestDigitalClockPage", "电子表", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = controlMenuItem
            });

            controlMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestPDFWebViewPage", "PDF阅读器", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = controlMenuItem
            });

            Models.MenuItem behaviorMenuItem = new Models.MenuItem("BehaviorGroup", "Behavior【行为】", ImageSource.FromFile("setting.png"));
            behaviorMenuItem.ParentMenuItem = baseMenuItem;
            behaviorMenuItem.ChildrenMenu = new List<Models.MenuItem>();
            list.Add(behaviorMenuItem);

            behaviorMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestAttachedBehaviorPage", "AttachedBehavior【附加行为】", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = behaviorMenuItem
            });

            behaviorMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestBehaviorsPage", "Behavior<T>【附加行为】", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = behaviorMenuItem
            });

            behaviorMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestBehaviorsPage2", "Behavior<T>【附加行为】2", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = behaviorMenuItem
            });

            behaviorMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestEffectBehaviorPage", "EffectBehavior【效果行为】", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = behaviorMenuItem
            });

            Models.MenuItem effectMenuItem = new Models.MenuItem("EffectGroup", "Effect【效果】", ImageSource.FromFile("setting.png"));
            effectMenuItem.ParentMenuItem = baseMenuItem;
            effectMenuItem.ChildrenMenu = new List<Models.MenuItem>();
            list.Add(effectMenuItem);

            effectMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestLabelShadowEffetPage", "LabelShadowEffet【Label阴影效果】", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = effectMenuItem
            });
            effectMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestDraggableBoxViewPage", "DraggableBoxView【可拖拽矩形】", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = effectMenuItem
            });

            Models.MenuItem animationMenuItem = new Models.MenuItem("Animation", "动画", ImageSource.FromFile("setting.png"));
            animationMenuItem.ParentMenuItem = baseMenuItem;
            animationMenuItem.ChildrenMenu = new List<Models.MenuItem>();
            list.Add(animationMenuItem);
            animationMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestBasicAnimationPage", "基础动画效果", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = animationMenuItem
            });
            animationMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestCustomAnimationPage", "自定义动画效果", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = animationMenuItem
            });

            Models.MenuItem pageMenuItem = new Models.MenuItem("PageGroup", "Page", ImageSource.FromFile("setting.png"));
            pageMenuItem.ParentMenuItem = baseMenuItem;
            pageMenuItem.ChildrenMenu = new List<Models.MenuItem>();
            list.Add(pageMenuItem);

            pageMenuItem.ChildrenMenu.Add(new Models.MenuItem("TestCarouselPage", "简单CarouselPage", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = controlMenuItem
            });

            pageMenuItem.ChildrenMenu.Add(new Models.MenuItem("Page1", "页面导航", ImageSource.FromFile("setting.png"))
            {
                ParentMenuItem = controlMenuItem
            });

            for (int i = 0; i < 5; i++)
            {
                String MenuItemId = "MenuItemId_" + i;
                String Title = "Title" + i;
                IList<Models.MenuItem> childList = new List<Models.MenuItem>();
                var model = new Models.MenuItem(MenuItemId, Title, ImageSource.FromFile("setting.png"));
                model.ChildrenMenu = childList;
                model.ParentMenuItem = baseMenuItem;
                list.Add(model);
                for (int j = 0; j < 15; j++)
                {
                    var childModel = new Models.MenuItem(MenuItemId + "_" + j, Title + "_" + j, ImageSource.FromFile("setting.png"));
                    childModel.ParentMenuItem = model;
                    childList.Add(childModel);
                }
            }
            return baseMenuItem;
        }

        public Page GetPage(String menuItemId)
        {
            switch (menuItemId)
            {
                case "HomePage":
                    return new WelcomePage();
                case "ListMenuPage":
                    return new ListMenuPage();
                case "ListMenuAnimatePage":
                    return new ListMenuAnimatePage();
                case "ListMenuAnimatePage2":
                    return new ListMenuAnimatePage2();
                case "ClassAndSubclassesPage":
                    return new Pages.ClassAndSubclassesPage();
                case "TestLabelPage":
                    return new Pages.Control.TestLabelPage();
                case "TestColorPage":
                    return new Pages.Control.TestColorPage();
                case "TestEntryPage":
                    return new Pages.Control.TestEntryPage();
                case "TestEditorPage":
                    return new Pages.Control.TestEditorPage();
                case "TestDigitalClockPage":
                    return new Pages.Control.TestDigitalClockPage();
                case "TestPDFWebViewPage":
                    return new Pages.Control.TestPDFWebViewPage();
                case "TestAttachedBehaviorPage":
                    return new Pages.Behavior.TestAttachedBehaviorPage();
                case "TestBehaviorsPage":
                    return new Pages.Behavior.TestBehaviorsPage();
                case "TestBehaviorsPage2":
                    return new Pages.Behavior.TestBehaviorsPage2();
                case "TestEffectBehaviorPage":
                    return new Pages.Behavior.TestEffectBehaviorPage();
                case "TestLabelShadowEffetPage":
                    return new Pages.Effect.TestLabelShadowEffetPage();
                case "TestDraggableBoxViewPage":
                    return new Pages.Effect.TestDraggableBoxViewPage();
                case "TestBasicAnimationPage":
                    return new Pages.Animation.TestBasicAnimationPage();
                case "TestCustomAnimationPage":
                    return new Pages.Animation.TestCustomAnimationPage();
                case "Page1":
                    return new Pages.NavigationPages.Page1();
                case "TestCarouselPage":
                    return new Pages.TestCarouselPage();
                default:
                    return new ErrorPage("错误", "未知菜单ID【" + menuItemId + "】");
            }
        }
    }
}
