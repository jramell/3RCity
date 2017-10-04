    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Structure that represent a load of garbage. 
/// This garbage can be found on the houses, on the trucks or in the landfill.
/// For the moment, this is not a monobehaviour.
/// </summary>
public class Garbage
{
    public enum Type {
        Ordinary,
        Paper,
        Glass,
        Metal
    }
    // -----------------------------------------------------------
    // Attributes
    // -----------------------------------------------------------
    
    /// <summary>
    /// Amount of ordinary garbage. This is the garbage that can't be recicled by any means.
    /// </summary>
    public int ordinary;

    /// <summary>
    /// Amount of paper and cardboard. Recyclable.
    /// </summary>
    public int paper;

    /// <summary>
    /// Amount of glass. Recyclable.
    /// </summary>
    public int glass;

    /// <summary>
    /// Amount of metal. Recyclable.
    /// </summary>
    public int metal;

    /// <summary>
    /// The total amount of garbage.
    /// </summary>
    public int Total
    {
        get { return ordinary + paper + metal + glass; }
    }

    // -----------------------------------------------------------
    // Methods
    // -----------------------------------------------------------

    public Garbage()
    {
        ordinary = 0;
        paper = 0;
        glass = 0;
        metal = 0;
    }

    public Garbage(int nRegular, int nPaper, int nGlass, int nMetal)
    {
        ordinary = nRegular;
        paper = nPaper;
        glass = nGlass;
        metal = nMetal;
    }

    /// <summary>
    /// Add the garbage given as parameter to the Garbage object that calls this method.
    /// </summary>
    /// <param name="garbage"></param>
    public void AddGarbage(Garbage garbage)
    {
        ordinary += garbage.ordinary;
        paper += garbage.paper;
        glass += garbage.glass;
        metal += garbage.metal;
    }
}
