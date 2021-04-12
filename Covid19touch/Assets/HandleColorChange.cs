using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleColorChange : MonoBehaviour
{
    public int woodTemp = 25;
    public int woodRH = 35;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ########Function BELOW IS FOR PLASTIC WHERE THE HALF-LIFE=6.81, HOURS=72, RH=65% AND TEMP=21-23 DEGREES CELSIUS########
    public double calculateSurvivalRatePlastic()
    {
        double t_hrs = 6.81; // This value is given in the half-life columnn t_hrs=T
        double tau_val; // tau_val=τ
        double lambda_val;
        lambda_val = Math.Log(2) / t_hrs;
        tau_val = 1 / lambda_val;

        // This code is for the equation N(t)=No*exp(-t/τ) this equation is survival time of virus on the surface

        double initial_val = .05;
        int time_var = 72;
        double exponent_val;
        exponent_val = (-time_var / tau_val);
        double inverse_exp;
        inverse_exp = Math.Exp(exponent_val);
        double final_val;
        final_val = initial_val * inverse_exp; // The is the final amount that is left of the virus after the certain half-life
        Console.WriteLine("  the final value with  " + t_hrs + "  half-life is  " + final_val);
        Console.ReadLine();
        return final_val;
    }

    public double calculateSurvivalRateSteel()
    {
        //##########EQUATION BELOW IS FOR STAINLESS STEEL WHERE HALF-LIFE=5.63, HOURS=72, RH=65% AND TEMP=21-23 DEGREES CELSIUS##########

        double t_hrs = 5.63; // This value is given in the half-life columnn t_hrs=T
        double tau_val; // tau_val=τ
        double lambda_val;
        lambda_val = Math.Log(2) / t_hrs;
        tau_val = 1 / lambda_val;

        // This code is for the equation N(t)=No*exp(-t/τ) this equation is survival time of virus on the surface

        double initial_val = .05;
        int time_var = 72;
        double exponent_val;
        exponent_val = (-time_var / tau_val);
        double inverse_exp;
        inverse_exp = Math.Exp(exponent_val);
        double final_val;
        final_val = initial_val * inverse_exp; // The is the final amount that is left of the virus after the certain half-life

        return final_val;
    }

    public double calculateSurvivalRateWood()
    {
        //###########EQUATION BELOW IS FOR WOOD WHERE HALF-LIFE=21.41, HOURS=168, RH=35% AND TEMP=25-27 DEGREES CELSIUS######


        double t_hrs = 21.41; // This value is given in the half-life columnn t_hrs=T
        double tau_val; // tau_val=τ
        double lambda_val;
        lambda_val = Math.Log(2) / t_hrs;
        tau_val = 1 / lambda_val;

        // This code is for the equation N(t)=No*exp(-t/τ) this equation is survival time of virus on the surface

        double initial_val = .05;
        int time_var = 168; // equates to 7 days
        double exponent_val;
        exponent_val = (-time_var / tau_val);
        double inverse_exp;
        inverse_exp = Math.Exp(exponent_val);
        double final_val;
        final_val = initial_val * inverse_exp; // The is the final amount that is left of the virus after the certain half-life
        return final_val;
    }
}
