using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControllerSoubra : MonoBehaviour
{
    public bool wrong = false;
    int lastBlockKeyId = 0;
    public int currentSelectionCount = 0;
    public PuzzleDataHajjo[] selectedBlocks;

    // Update is called once per frame
    void Update()
    {
    }

    private void Start()
    {
        selectedBlocks = new PuzzleDataHajjo[9];
    }

    public void TurnPuzzle(PuzzleDataHajjo puzzleObject)
    {
        PuzzleDataHajjo puzzleBlockInfo = puzzleObject;

        // if the block is not yet solved and not facing
        if (!puzzleBlockInfo.solved && !puzzleBlockInfo.facing)
        {


            selectedBlocks[currentSelectionCount] = puzzleBlockInfo;
            puzzleBlockInfo.anim.Play();
            puzzleBlockInfo.facing = true;
            // it happens while you click



            if (currentSelectionCount > 0 && lastBlockKeyId != puzzleBlockInfo.keyType)
            {
                wrong = true;
            }

            lastBlockKeyId = puzzleBlockInfo.keyType;
            currentSelectionCount++;

            if (currentSelectionCount == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (wrong)
                    {
                        selectedBlocks[i].Reset();
                    }

                    else selectedBlocks[i].solved = true;

                }
                currentSelectionCount = 0;
                wrong = false;
            }
        }
    }
}
