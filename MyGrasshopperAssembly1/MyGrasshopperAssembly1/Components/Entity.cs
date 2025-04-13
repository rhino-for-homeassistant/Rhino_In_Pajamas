using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Hub
{
    public class Entity: GH_Component
    {
        /// <summary>
            // Define a uniquely identifiable element (e.g., a device, sensor, or service)
            // that can be monitored, controlled, or automated.
        /// </summary>
        public Entity()
          : base(
                "Entity",
                "Entity",
                "Define a uniquely identifiable element (e.g., a device, sensor, or service) that can be monitored, controlled, or automated.",
                 PluginUtilities.TabName,
                 PluginUtilities.CategoryAAInputs
                )
        {
        }

        public override void CreateAttributes()
        {
            if (first)
            {
                FunctionToSetSelectedContent(0, 0);
                first = false;
            }
            m_attributes = new CustomUI.DropDownUIAttributes(this, FunctionToSetSelectedContent, dropdowncontents, selections, spacerDescriptionText);

        }

        public void FunctionToSetSelectedContent(int dropdownListId, int selectedItemId)
        {
            // on first run we create the combined dropdown content
            if (dropdowncontents == null)
            {
                // create list to populate dropdown content with
                dropdowncontents = new List<List<string>>(); //clear all previous content
                selections = new List<string>();

                dropdowncontents.Add(dropdownTopLevelContent); //add Top Level content as first list
                selections.Add(dropdownTopLevelContent[0]);

                dropdowncontents.Add(dropdownLevel2_A_Content); //add level 2 first list as default on first run
                selections.Add(dropdownLevel2_A_Content[0]);

                // add the lists corrosponding to top level content order
                dropdownLevel2_Content.Add(dropdownLevel2_A_Content);
                dropdownLevel2_Content.Add(dropdownLevel2_B_Content);
                dropdownLevel2_Content.Add(dropdownLevel2_C_Content);
                dropdownLevel2_Content.Add(dropdownLevel2_D_Content);
            }

            if (dropdownListId == 0) // if change is made to first list
            {
                // change the content of level 2 based on selection
                dropdowncontents[1] = dropdownLevel2_Content[selectedItemId];

                // update the shown selected to first item in list
                selections[1] = dropdowncontents[1][0];
            }

            if (dropdownListId == 1) // if change is made to second list
            {
                selections[1] = dropdowncontents[1][selectedItemId];

                // do something with the selected item
                System.Windows.Forms.MessageBox.Show("You selected: " + dropdowncontents[1][selectedItemId]);
            }

            // for Grasshopper to redraw the component to get changes to dropdown menu displayed on canvas:
            Params.OnParametersChanged();
            ExpireSolution(true);
        }

        #region dropdownmenu content
        // this region is where (static) lists are created that will be displayed
        // in the dropdown menus dependent on user selection.

        List<List<string>> dropdowncontents; // list that holds all dropdown contents
        List<List<string>> dropdownLevel2_Content = new List<List<string>>(); // list to hold level2 content

        List<string> selections; // list of the selected items 
        bool first = true; // bool to create menu first time the component runs

        readonly List<string> spacerDescriptionText = new List<string>(new string[]
        {
            "Type",
            "Type Details"
        });
        readonly List<string> dropdownTopLevelContent = new List<string>(new string[]
        {
            "Light",
            "Sensor",
            "Monitor",
            "Lock"
        });
        // lists longer than 10 will automatically get a vertical scroll bar
        readonly List<string> dropdownLevel2_A_Content = new List<string>(new string[]
        {
            "RGB Bulbs",
            "Incandescent Bulbs",
            "Halogen Bulbs",
            "Compact Fluorescent Lights (CFLs)",
            "LED Bulbs",
            "Smart Bulbs",
            "Light Strips",
            "Specialty Lights",
        });

        readonly List<string> dropdownLevel2_B_Content = new List<string>(new string[]
        {
            "Temperature and Humidity Sensors",
            "Motion Sensors (PIR, Microwave, Ultrasonic)",
            "Light Sensors (Photoresistors, Photodiodes)",
            "Proximity Sensors",
            "Pressure Sensors",
            "Smoke, CO, and CO₂ Sensors",
            "Contact Sensors",
            "Vibration Sensors",
            "Water Leak/Level Sensors",
            "Environmental Sensors (Air Quality, Dust, PM2.5)",
        });

        readonly List<string> dropdownLevel2_C_Content = new List<string>(new string[]
        {
            "Energy/Power Monitors",
            "Network Monitors",
            "System Resource Monitors",
            "Camera/Video Monitors",
            "Environmental Monitors",
            "Appliance/Device Usage Monitors",
            "Health Monitors",
        });

        readonly List<string> dropdownLevel2_D_Content = new List<string>(new string[]
        {
            "Deadbolt Locks",
            "Cylinder Locks",
            "Mortise Locks",
            "Padlocks",
            "Smart Locks",
            "Biometric Locks",
            "Electromagnetic / Electric Strike Locks",
        });
        #endregion

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {

        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Type", "T", "Type group of Entity", GH_ParamAccess.item);
            pManager.AddGenericParameter("Details", "D", "Details of Entity type group", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            DA.SetData(0, selections[0]);
            DA.SetData(1, selections[1]);
        }

        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override System.Drawing.Bitmap Icon => null;

        public override Guid ComponentGuid => new Guid("6ACAA256-00CB-46ED-ACE7-1FDAFD0A426B");
    }
}
