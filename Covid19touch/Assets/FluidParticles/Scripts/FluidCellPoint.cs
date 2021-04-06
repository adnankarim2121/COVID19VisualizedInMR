using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidCellPoint  {
    //This stores the position to place the cell on the canvas
    public float x;
    public float y;

    //This is the width and height of the cell
    public float r;

    //These are the attributes that will hold the row and column values
    public int col = 0;
    public int row = 0;

    //This stores the cell's velocity
    public float xv = 0;
    public float yv = 0;

    //This is the pressure attribute
    public float pressure = 0;

    public FluidCellPoint up;
    public FluidCellPoint left;
    public FluidCellPoint up_left;
    public FluidCellPoint up_right;

    public FluidCellPoint down;
    public FluidCellPoint right;
    public FluidCellPoint down_right;
    public FluidCellPoint down_left;


    public FluidCellPoint(float x,float y,float res)
    {
        this.x = x;
        this.y = y;
        this.r = res;
    }

    public void Reset()
    {
        xv = 0;
        yv = 0;
        pressure = 0;
    }
}
