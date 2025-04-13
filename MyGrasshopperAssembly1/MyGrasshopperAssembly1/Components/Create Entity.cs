using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Display;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;  // For setting color

namespace Rhino.Hub
{
    public class DisplayLight : GH_Component
    {
        public DisplayLight()
          : base(
                "Display Light",
                "DisplayLight",
                "Display Entity",
                PluginUtilities.TabName,
                PluginUtilities.CategoryAAInputs
                )
        {
        }

        List<Cone> lightGeometry = new List<Cone>();
        List<Point3d> lightLocations = new List<Point3d>();
        System.Drawing.Color lightColor;
        int lightIntensity = 1;
        bool lightEnabled = false;

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGeometryParameter("Geometry", "G", "Entity Geometry to add attributes to", GH_ParamAccess.list);
            pManager.AddColourParameter("Color", "C", "Light Color (Choose a color for the light)", GH_ParamAccess.item);  // Color input
            pManager.AddBooleanParameter("State", "S", "State of Entity (True=On, False=Off)", GH_ParamAccess.item);  // Light state (on or off)
            pManager.AddNumberParameter("Intensity", "I", "Intensity of Light", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
         
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            //Inputs
            List<GeometryBase> geometryList = new List<GeometryBase>();
            if (!DA.GetDataList(0, geometryList)) return;

            System.Drawing.Color color = System.Drawing.Color.White;
            if (!DA.GetData(1, ref color)) return;

            bool state = true;
            if (!DA.GetData(2, ref state)) return;

            double intensity = 1;
            if (!DA.GetData(3, ref intensity)) return;

            //Get Centroids
            List<Point3d> centroids = new List<Point3d>();

            foreach (var geometry in geometryList)
            {
                BoundingBox bbox = geometry.GetBoundingBox(true);
                Point3d centroid = bbox.Center;
                centroids.Add(centroid);
            }

            //Get Light Geometry
            List<Cone> lightCones = new List<Cone>();
         
            foreach (Point3d centroid in centroids)
            {
                Plane basePlane = new Plane(centroid, -Vector3d.ZAxis); ;
                double height = 3.4;
                double radius = intensity;
                Cone cone = new Cone(basePlane, height, radius);
                lightCones.Add(cone);
            }

            //Save Data to render
            lightGeometry = lightCones;
            lightLocations = centroids;
            lightColor = color;
            lightEnabled = state;
        }

        public override void DrawViewportMeshes(IGH_PreviewArgs args)
        {
            base.DrawViewportMeshes(args);

            for (int i = 0; i < lightGeometry.Count; i++)
            {
                if (lightEnabled)
                {
                    Brep coneBrep = lightGeometry[i].ToBrep(true);

                    // Create a DisplayMaterial with the desired color and transparency
                    DisplayMaterial material = new DisplayMaterial(lightColor);
                    material.Transparency = 0.5f; // Set transparency level (0 = opaque, 1 = fully transparent)

                    // Apply the DisplayMaterial and render the Brep
                    args.Display.DrawBrepShaded(coneBrep, material);
                }
            }
        }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override System.Drawing.Bitmap Icon => null;

        public override Guid ComponentGuid => new Guid("1217D512-30A8-475D-955C-C28B126849B2");
    }
}
