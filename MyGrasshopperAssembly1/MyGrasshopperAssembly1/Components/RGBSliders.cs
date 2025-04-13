using System;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Windows.Forms;

namespace Rhino.Hub
{
    public class RGBSliderComponent : GH_Component
    {
        // Constructor
        public RGBSliderComponent() : base("RGB Sliders", "RGB", "Three sliders for R, G, B values", PluginUtilities.TabName,
                     PluginUtilities.CategoryAAInputs)
        { }

        // Register input and output parameters
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("R", "R", "Red Value", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("G", "G", "Green Value", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("B", "B", "Blue Value", GH_ParamAccess.item, 0.0);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddColourParameter("Color", "Color", "RGB color output", GH_ParamAccess.item);
        }

        // Solve instance (must be protected to override)
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double r = 0, g = 0, b = 0;
            if (!DA.GetData(0, ref r)) return;
            if (!DA.GetData(1, ref g)) return;
            if (!DA.GetData(2, ref b)) return;

            // Output the color
            System.Drawing.Color color = System.Drawing.Color.FromArgb(
                (int)(r * 255),
                (int)(g * 255),
                (int)(b * 255)
            );
            DA.SetData(0, color);
        }

        // Description of the component (appear in Grasshopper)
        public override Guid ComponentGuid => new Guid("0f4b4f3e-1b71-4386-86d1-c7a28728c3e7");

        // Custom drawing (optional customization)
        // Here you can add any custom code if needed for menu or interface.
    }
}
