using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPanelManager : MonoBehaviour
{
    public GameObject main_instruction_panel;
    public GameObject vc_ins_Panel_1;
    public GameObject vc_ins_Panel_2;
    public GameObject vc_ins_Panel_3;
    public GameObject hc_ins_panel_1;
    public GameObject hc_ins_panel_2;
    public GameObject hc_ins_panel_3;
    
    // virtual controller panel activate inactivate handler
    public void VC_Panel_deactivate()
    {
        vc_ins_Panel_1.SetActive(false);

    }
    public void VC_Panel_activate()
    {
        vc_ins_Panel_1.SetActive(true);

    }
    public void VC_Panel_2_deactivate()
    {
        vc_ins_Panel_2.SetActive(false);

    }
    public void VC_Panel_2_activate()
    {
        vc_ins_Panel_2.SetActive(true);

    }
    public void VC_Panel_3_deactivate()
    {
        vc_ins_Panel_3.SetActive(false);

    }
    public void VC_Panel_3_activate()
    {
        vc_ins_Panel_3.SetActive(true);

    }
    
    // hardware controller panel activate inactivate handler
    public void HC_Panel_deactivate()
    {
        hc_ins_panel_1.SetActive(false);

    }
    public void HC_Panel_activate()
    {
        hc_ins_panel_1.SetActive(true);

    }
    public void HC_Panel_2_deactivate()
    {
        hc_ins_panel_2.SetActive(false);

    }
    public void HC_Panel_2_activate()
    {
        hc_ins_panel_2.SetActive(true);

    }
    public void HC_Panel_3_deactivate()
    {
        hc_ins_panel_3.SetActive(false);

    }
    public void HC_Panel_3_activate()
    {
        hc_ins_panel_3.SetActive(true);

    }

    // main menu panel activate inactivate handler

    public void main_Panel_deactivate()
    {
        main_instruction_panel.SetActive(false);

    }
    public void main_Panel_activate()
    {
        main_instruction_panel.SetActive(true);

    }


    
}

