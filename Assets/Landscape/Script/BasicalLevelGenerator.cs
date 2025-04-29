using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BasicalLevelGenerator : MonoBehaviour
{
    public GameObject theme;
    public GameObject classicBlock;
    public GameObject waterObstacle;
    public GameObject groundObstacle;
    public GameObject ceilingObstalce;
    public Canvas backbground;

    public int levelLength;
    public int levelHeight;
    public int x;
    public int y;

    // varriable to prevent suite of obstaclees
    private int groundObstacleWait = 0;
    private int lastGrounLevel;


    public void generate()
    {
        // we get the ThemeTemplate component
        ThemeTemplate theme = this.theme.GetComponent<ThemeTemplate>();

        // setting the background texture
        setBackgroundTexture(theme);

        bool isCeilingBefore = false;
        groundObstacleWait = 0;
        // instanciate lastgroundlevel
        lastGrounLevel = (int)Math.Round(levelHeight * 0.3);
        for (int i = 0; i < levelLength; i++)
        {
            int lastJ = 0;

            // we separate the level in 3 diifferents layers
            // the undeground, the the ground and the ceiling
            lastJ = generateUnderground(i, theme);
            lastJ += 1;
            lastJ = generateGround(i, theme, lastJ);
            lastJ += 1;
            isCeilingBefore = generateCeiling(i, theme, lastJ, isCeilingBefore);

        }
    }

    private int generateUnderground(int i, ThemeTemplate theme)
    {
        int lastJ = 0;
        // the underground represent the 30% down of the level
        for (int j = 0; j < levelHeight * 0.3; j++)
        {
            GameObject groundBlock = Instantiate(classicBlock, new Vector2(i +x, j +y), Quaternion.identity, this.transform);
            groundBlock.GetComponent<SpriteRenderer>().sprite = theme.deepTextures[UnityEngine.Random.Range(0, theme.deepTextures.Length)];

            // updating last J
            lastJ = j;
        }
        return lastJ;
    }

    private int generateGround(int i, ThemeTemplate theme, int startingJ)
    {
        int groundObstacleWaitTime = 2;

        int lastJ = startingJ;

        // the groud represent the 40% midle of the level
        double stopingCondition = startingJ + levelHeight * 0.4;

        bool isBlockUnder = true;

        int currentGroundLevel = lastGrounLevel;

        for (int j = startingJ; j < stopingCondition; j++)
        {
            // keep a 2 free space on top of ground
            if ((j + 1 > stopingCondition) || j > lastGrounLevel + 1)
            {
                // instanciate nothing for letting space to player
                //  j > lastGrounLevel + 2 --> avoid the formation of impassable "towers"
                lastJ = j + 1;
                break;
            }
            else
            {
                if (isBlockUnder)
                {
                    if (UnityEngine.Random.Range(0, 100) > 30 + j * levelHeight * 0.1)
                    {
                        Debug.Log("Last ground level");
                        Debug.Log(lastGrounLevel);

                        isBlockUnder = true;

                        GameObject block = Instantiate(classicBlock, new Vector2(i + x, j + y), Quaternion.identity, this.transform);
                        block.GetComponent<SpriteRenderer>().sprite = theme.groundTextures[UnityEngine.Random.Range(0, theme.groundTextures.Length)];
                        // Updating the lastGrounLevel 
                        currentGroundLevel = j;


                    } else
                    {
                        isBlockUnder = false;
                        currentGroundLevel = j - 1;

                        if (groundObstacleWait <= 0)
                        {
                            if (UnityEngine.Random.Range(0,100) > 25) 
                            {
                                groundObstacleWait = groundObstacleWaitTime;
                                Debug.Log("AAA");
                                Debug.Log(groundObstacle.transform.localScale.y);
                                GameObject obstacle = Instantiate(groundObstacle, new Vector2(i +x, j +y - (classicBlock.transform.localScale.y - groundObstacle.transform.localScale.y)/2), Quaternion.identity, this.transform);
                                obstacle.GetComponent<SpriteRenderer>().sprite = theme.groundObstacleTextures[UnityEngine.Random.Range(0, theme.groundObstacleTextures.Length)];
                            } else
                            {
                                --groundObstacleWait;
                            }
                        } else
                        {
                            --groundObstacleWait;
                        }
                    }
                }

            }
            // updating last J
            lastJ = j;
        }
        // updating the lastGroundLevel for future iterations
        lastGrounLevel = currentGroundLevel;
        Debug.Log("last J");
        Debug.Log(lastJ);
        return lastJ;
    }

    private bool generateCeiling(int i, ThemeTemplate theme, int startingJ, bool isCeilingBefore)
    {
        int lastJ = startingJ;
        bool isCeilingNow = false;

        // only the first blocck of ceiling determines if there is ceiling
        if (startingJ + 1 < levelHeight)
        {
            if (isCeilingBefore)
            {
                if (UnityEngine.Random.Range(0, 100) > 10) 
                {
                    isCeilingNow = true;

                    GameObject ceilingBlock = Instantiate(classicBlock, new Vector2(i +x, startingJ + 1 +y), Quaternion.identity, this.transform);
                    ceilingBlock.GetComponent<SpriteRenderer>().sprite = theme.ceilingTextures[UnityEngine.Random.Range(0, theme.ceilingTextures.Length)];
                    // updating last J
                    lastJ = startingJ + 1;
                }
            }
            else
            {
                if (UnityEngine.Random.Range(0, 100) > 80)
                {
                    isCeilingNow = true;

                    GameObject ceilingBlock = Instantiate(classicBlock, new Vector2(i +x, startingJ + 1 +y), Quaternion.identity, this.transform);
                    int randomIntLa = UnityEngine.Random.Range(0, theme.ceilingTextures.Length);
                    ceilingBlock.GetComponent<SpriteRenderer>().sprite = theme.ceilingTextures[randomIntLa];
                    // updating last J
                    lastJ = startingJ + 1;
                }
            }
        }


        if (isCeilingNow)
        {
            if (UnityEngine.Random.Range(0,100) > 65)
            {
                GameObject obstalce = Instantiate(ceilingObstalce, new Vector2(i +x, startingJ + y), Quaternion.identity, this.transform);
                obstalce.GetComponent<SpriteRenderer>().sprite = theme.ceilingObstacleTextures[UnityEngine.Random.Range(0, theme.ceilingObstacleTextures.Length)];
            }
            /**
            for (int j = startingJ + 2; j < levelHeight; j++)
            {
                if (UnityEngine.Random.Range(0, 100) > 30)
                {
                    GameObject ceilingBlock = Instantiate(classicBlock, new Vector2(i + x, j + y), Quaternion.identity, this.transform);
                    ceilingBlock.GetComponent<SpriteRenderer>().sprite = theme.ceilingTextures[UnityEngine.Random.Range(0, theme.ceilingTextures.Length)];
                }


                // updating last J
                lastJ = j;
            }
            **/

        }


        return isCeilingNow;
    }

    private void setBackgroundTexture(ThemeTemplate theme)
    {
        Image bgImage = backbground.GetComponentInChildren<Image>();
        bgImage.sprite = theme.background;
    }

    private void Start()
    {
        Debug.Log("oeoeoe");
        generate();
        Debug.Log("oeuf");

    }
}
