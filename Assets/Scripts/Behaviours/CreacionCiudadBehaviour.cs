using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class CreacionCiudadBehaviour : MonoBehaviour
{

    public GameObject pisoNormal;
    public GameObject pisoFabrica;
    public GameObject pisoArbol1;
    public GameObject pisoArbol2;
    public GameObject pisoAlargado;
    public GameObject pisoVertedero;
    public GameObject casaFrente;
    public GameObject casaLado;
    public GameObject pisoPasto4x4;
    public GameObject agua;
    public GameObject pisoMuerto;
    private String[][] matrizCiudad;
    public TextAsset TextFile;

    void Start () {
        readTextFileLines();
        for (int i = 0; i < matrizCiudad.Length; i++)
        {
            for (int j = 0; j < matrizCiudad[i].Length; j++)
            {
                //Crea el piso normal donde se pueden colocar objectos de tamaño 1
                if (matrizCiudad[i][j].Equals("P1"))
                {
                    Instantiate(pisoNormal, new Vector3(i*4, 0, j*4), Quaternion.identity);
                }
                //Crea un piso donde se colocan objectos de tamaño 4
                else if (matrizCiudad[i][j].Equals("P2"))
                {
                    if (j < (matrizCiudad[i].Length-1) && i < (matrizCiudad.Length-1))
                    {
                        if (matrizCiudad[i + 1][j].Equals("P2") && matrizCiudad[i + 1][j + 1].Equals("P2") && matrizCiudad[i][j + 1].Equals("P2"))
                        {
                            Instantiate(pisoFabrica, new Vector3(i * 4, 0, j * 4), Quaternion.identity);
                            matrizCiudad[i + 1][j] = "U";
                            matrizCiudad[i + 1][j+1] = "U";
                            matrizCiudad[i][j+1] = "U";
                        }
                    }      
                }
                //Crea el piso que contiene un arbol
                else if (matrizCiudad[i][j].Equals("PA1"))
                {
                    Instantiate(pisoArbol1, new Vector3(i * 4, 0, j * 4), Quaternion.identity);
                }
                else if (matrizCiudad[i][j].Equals("CF"))
                {
                    Instantiate(casaFrente, new Vector3(i * 4, 0, j * 4), Quaternion.identity);
                }
                else if (matrizCiudad[i][j].Equals("CL"))
                {
                    Instantiate(casaLado, new Vector3(i * 4, 0, j * 4), Quaternion.identity);
                }
                //Crea el piso que contiene un piso de tamañp1.5 para edificios alargados
                else if (matrizCiudad[i][j].Equals("P3"))
                {
                    if (i < (matrizCiudad.Length-1))
                    {
                        if (matrizCiudad[i + 1][j].Equals("P3"))
                        {
                            Instantiate(pisoAlargado, new Vector3(i * 4, 0, j * 4), Quaternion.identity);
                            matrizCiudad[i + 1][j] = "U";            
                        }
                    }
                }
                else if (matrizCiudad[i][j].Equals("PV"))
                {
                    if (j < (matrizCiudad[i].Length - 1) && i < (matrizCiudad.Length - 1))
                    {
                        if (matrizCiudad[i + 1][j].Equals("PV") && matrizCiudad[i + 1][j + 1].Equals("PV") && matrizCiudad[i][j + 1].Equals("PV"))
                        {
                            Instantiate(pisoVertedero, new Vector3(i * 4, 0, j * 4), Quaternion.identity);
                            matrizCiudad[i + 1][j] = "U";
                            matrizCiudad[i + 1][j + 1] = "U";
                            matrizCiudad[i][j + 1] = "U";
                        }
                    }
                }
                else if (matrizCiudad[i][j].Equals("PA4"))
                {
                    if (j < (matrizCiudad[i].Length - 1) && i < (matrizCiudad.Length - 1))
                    {
                        if (matrizCiudad[i + 1][j].Equals("PA4") && matrizCiudad[i + 1][j + 1].Equals("PA4") && matrizCiudad[i][j + 1].Equals("PA4"))
                        {
                            Instantiate(pisoPasto4x4, new Vector3(i * 4, 0, j * 4), Quaternion.identity);
                            matrizCiudad[i + 1][j] = "U";
                            matrizCiudad[i + 1][j + 1] = "U";
                            matrizCiudad[i][j + 1] = "U";
                        }
                    }
                }
                else if (matrizCiudad[i][j].Equals("AG"))
                {
                    if (j < (matrizCiudad[i].Length - 1) && i < (matrizCiudad.Length - 1))
                    {
                        if (matrizCiudad[i + 1][j].Equals("AG") && matrizCiudad[i + 1][j + 1].Equals("AG") && matrizCiudad[i][j + 1].Equals("AG"))
                        {
                            Instantiate(agua, new Vector3(i * 4, 0, j * 4), Quaternion.identity);
                            matrizCiudad[i + 1][j] = "U";
                            matrizCiudad[i + 1][j + 1] = "U";
                            matrizCiudad[i][j + 1] = "U";
                        }
                    }
                }
                else if (matrizCiudad[i][j].Equals("PM"))
                {
                    Instantiate(pisoMuerto, new Vector3(i * 4, 0, j * 4), Quaternion.identity);
                }
            }
        }
    }

    void readTextFileLines()
    {
        string[] linesInFile = TextFile.text.Split('\n');
        matrizCiudad = new string[linesInFile.Length][];
        int i = 0;
        foreach (string line in linesInFile)
        {
            string[] partesCiudad = line.Split(',');   
            matrizCiudad[i] = new string[partesCiudad.Length];
            for (int j = 0; j < partesCiudad.Length; j++)
            {
                 matrizCiudad[i][j] = partesCiudad[j].Trim();
            }
            i++;
        }
    }

}
