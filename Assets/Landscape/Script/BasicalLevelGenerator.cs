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

    // portals
    public GameObject endPortal;
    public GameObject[] bonusPortals;
    private const int bonusWait = 15;
    private int bonusCouldown = bonusWait;

    public int levelLength;
    public int levelHeight;
    public int x;
    public int y;

    private double groundSize;
    private double ceilingSize;

    // varriable to prevent suite of obstaclees
    private int groundObstacleWait;
    private const int groundObstacleWaitTime = 2;
    private int lastGrounLevel;


    public void generate()
    {
        // we get the ThemeTemplate component
        ThemeTemplate theme = this.theme.GetComponent<ThemeTemplate>();

        // setting the background texture
        setBackgroundTexture(theme);

        bool isCeilingBefore = false;
        groundObstacleWait = 5;
        // instanciate lastgroundlevel
        lastGrounLevel = (int)Math.Round(groundSize);

        // start by genering the platform at the start of the level
        generateStartPlatform(theme);

        // star genering the real level
        for (int i = 0; i < levelLength; i++)
        {
            int lastJ = 0;

            // we separate the level in 3 diifferents layers
            // the undeground, the the ground and the ceiling
            lastJ = generateUnderground(ref i, theme);
            lastJ += 1;
            lastJ = generateGround(ref i, theme, lastJ);
            lastJ += 2;
            isCeilingBefore = generateCeiling(ref i, theme, lastJ, isCeilingBefore);

        }

        // Adding END PORTAL
        GameObject portal = Instantiate(endPortal, new Vector2(levelLength + 1 + x, y), Quaternion.identity, this.transform);
        float yScale = levelHeight / 3;
        float xScale = yScale * 2 / 3;

        portal.transform.localScale = new Vector2(xScale, yScale);

        // Setting the textures from the theme
        setPortalTextures(portal, theme);
    }

    private void generateStartPlatform(ThemeTemplate theme)
    {
        for (int i = -30; i < 0; i++)
        {
            generateUnderground(ref i, theme);
        }
    }

    private int generateUnderground(ref int i, ThemeTemplate theme)
    {
        int lastJ = 0;
        // the underground represent the 30% down of the level
        for (int j = 0; j < groundSize; j++)
        {
            GameObject groundBlock = Instantiate(classicBlock, new Vector2(i +x, j +y), Quaternion.identity, this.transform);
            groundBlock.GetComponent<SpriteRenderer>().sprite = theme.deepTextures[UnityEngine.Random.Range(0, theme.deepTextures.Length)];

            // updating last J
            lastJ = j;
        }
        return lastJ;
    }

    private int generateGround(ref int i, ThemeTemplate theme, int startingJ)
    {

        int lastJ = startingJ;

        // the groud represent the 40% midle of the level
        double stopingCondition = startingJ + ceilingSize;

        bool isBlockUnder = true;

        int currentGroundLevel = lastGrounLevel;

        // creating a bonus portal if it's possible
        if (bonusCouldown <= 0 && UnityEngine.Random.Range(0, 100) > 80)
        {
            // reset the bonus  couldown
            bonusCouldown = bonusWait;

            GameObject portalToInstanciate = bonusPortals[UnityEngine.Random.Range(0, bonusPortals.Length)];

            GameObject portal = Instantiate(portalToInstanciate, new Vector2(i + x,currentGroundLevel + -0.5f + y), Quaternion.identity, this.transform);
            float yScale = ((float)stopingCondition - currentGroundLevel) / 3;
            float xScale = yScale * 2 / 3;
            portal.transform.localScale = new Vector2(xScale, yScale);
            // Setting the textures from the theme
            setPortalTextures(portal, theme);

            // the space to let free for the portal
            float portalGroundSpace = xScale * 3 + 1;
            // instanciating portal pdestal
            if (currentGroundLevel > (int)Math.Round(groundSize))
            {
                // letting free space for the portal
                for (int k = 0; k < portalGroundSpace; k++)
                {
                    // filling the ground under portal
                    for (int h = startingJ; h < currentGroundLevel; h++)
                    {
                        GameObject block = Instantiate(classicBlock, new Vector2(i + x, h + y), Quaternion.identity, this.transform);
                        block.GetComponent<SpriteRenderer>().sprite = theme.groundTextures[UnityEngine.Random.Range(0, theme.groundTextures.Length)];
                    }

                    // updating the global i
                    generateUnderground(ref i, theme);
                    ++i;
                }
                --i;
            } else
            {
                // letting free space for the portal
                for (int k = 0; k < portalGroundSpace; k++)
                {
                    generateUnderground(ref i, theme);
                    ++i;
                }
                --i;
            }

            lastJ = (int)stopingCondition;
        }
        else
        {

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
                            isBlockUnder = true;

                            GameObject block = Instantiate(classicBlock, new Vector2(i + x, j + y), Quaternion.identity, this.transform);
                            block.GetComponent<SpriteRenderer>().sprite = theme.groundTextures[UnityEngine.Random.Range(0, theme.groundTextures.Length)];
                            // Updating the lastGrounLevel 
                            currentGroundLevel = j;


                        }
                        else
                        {
                            isBlockUnder = false;
                            currentGroundLevel = j - 1;

                            if (groundObstacleWait <= 0)
                            {
                                if (UnityEngine.Random.Range(0, 100) > 25)
                                {
                                    groundObstacleWait = groundObstacleWaitTime;

                                    GameObject obstacle = Instantiate(groundObstacle, new Vector2(i + x, j + y - (classicBlock.transform.localScale.y - groundObstacle.transform.localScale.y) / 2), Quaternion.identity, this.transform);
                                    obstacle.GetComponent<SpriteRenderer>().sprite = theme.groundObstacleTextures[UnityEngine.Random.Range(0, theme.groundObstacleTextures.Length)];
                                }
                                else
                                {
                                    --groundObstacleWait;
                                }
                            }
                            else
                            {
                                --groundObstacleWait;
                            }
                        }
                    }

                }
                // updating last J
                lastJ = j;
            }
        }
        // updating the lastGroundLevel for future iterations
        lastGrounLevel = currentGroundLevel;
        // updating the bonusCouldown for future itterations
        bonusCouldown -= 1;

        return lastJ;
    }

    private bool generateCeiling(ref int i, ThemeTemplate theme, int startingJ, bool isCeilingBefore)
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

    private void setPortalTextures(GameObject portal, ThemeTemplate theme)
    {
        GameObject borders = portal.transform.Find("Borders").gameObject;
        GameObject centers = portal.transform.Find("Center").gameObject;

        // setting textures for externals blocks
        foreach (Transform border in borders.transform)
        {
            border.gameObject.GetComponent<SpriteRenderer>().sprite = theme.portalBorderTexture;
        }

        //setting textures for inner blocks
        foreach (Transform center in centers.transform)
        {
            center.gameObject.GetComponent<SpriteRenderer>().sprite = theme.portalCenterTexture;
        }
    }

    private void Start()
    {
        // isntanciate variables
        groundSize = levelHeight * 0.3;
        ceilingSize = levelHeight * 0.4;

        generate();
    }
}
