using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControllerHajjo : MonoBehaviour
{
    public bool fuckedup = false;
    int lastBlockKeyId = 0;
    public int currentSelectionCount = 0;
    public PuzzleDataHajjo[] selectedBlocks;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.CompareTag("MemoryPuzzle"))
            {
                /*
                                Animation anim = hit.collider.gameObject.GetComponent<Animation>();
                                anim.Play();

                                selectedBlocks[currentSelectionCount] = hit.collider.gameObject;


                                if (currentSelectionCount > 1 )
                                {
                                    if (selectedBlocks[currentSelectionCount].name != selectedBlocks[currentSelectionCount - 1].name)
                                    {
                                        for (int i = 0; i < 3; i++)
                                        {
                                            Animation anim1 = selectedBlocks[i].GetComponent<Animation>();
                                            anim1.Play("TurnPuzzleCube_Back");
                                            selectedBlocks[i] = null;
                                        }
                                        currentSelectionCount = 0;
                                    }

                                    else if (selectedBlocks[currentSelectionCount].name == selectedBlocks[currentSelectionCount - 1].name)
                                    {
                                        for (int i = 0; i < 3; i++)
                                        {
                                            selectedBlocks[i] = null;
                                        }
                                        currentSelectionCount = 0;
                                    }
                                }

                                else currentSelectionCount += 1;
                */

                PuzzleDataHajjo puzzleBlockInfo = hit.collider.GetComponent<PuzzleDataHajjo>();

                // if the block is not yet solved and not facing
                if (!puzzleBlockInfo.solved && !puzzleBlockInfo.facing)
                {
              

                    selectedBlocks[currentSelectionCount] = puzzleBlockInfo;
                    puzzleBlockInfo.anim.Play();
                    puzzleBlockInfo.facing = true;
                    // it happens while you click


                
                    if (currentSelectionCount > 0 && lastBlockKeyId != puzzleBlockInfo.keyType)
                    {
                        fuckedup = true;
                    }

                    lastBlockKeyId = puzzleBlockInfo.keyType;


                    currentSelectionCount++;

                    if (currentSelectionCount == 3)
                    {
                        // reset the system
                       
                            // turn back all;
                            for (int i = 0; i < 3; i++)
                            {
                            if (fuckedup)
                            {
                                selectedBlocks[i].Reset();
                            }

                            else selectedBlocks[i].solved = true;

                            }
                        currentSelectionCount = 0;
                        fuckedup = false;
                    }
                    }
                   
                }

            }
        }
      
    }

