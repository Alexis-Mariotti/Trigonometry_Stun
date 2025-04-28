using UnityEngine;
using UnityEngine.UIElements;

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


    public void generate()
    {
        // we get the ThemeTemplate component
        ThemeTemplate theme = this.theme.GetComponent<ThemeTemplate>();

        // setting the background texture
        setBackgroundTexture(theme);

        for (int i = 0; i < levelLength; i++)
        {
            int lastJ = 0;
            bool isCeilingBefore = false;
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
            GameObject groundBlock = Instantiate(classicBlock, new Vector2(i, j), Quaternion.identity);
            groundBlock.transform.parent = this.transform;
            groundBlock.GetComponent<SpriteRenderer>().material.mainTexture = theme.deepTextures[Random.Range(0, theme.deepTextures.Length)];
            // updating last J
            lastJ = j;
        }
        return lastJ;
    }

    private int generateGround(int i, ThemeTemplate theme, int startingJ)
    {
        int lastJ = startingJ;

        // the groud represent the 40% midle of the level
        double stopingCondition = startingJ + levelHeight * 0.4;

        bool isBlockUnder = true;
        bool isObstacleBefore = false;

        for (int j = startingJ; j < stopingCondition; j++)
        {
            // keep a 2 free space on top of ground
            if (j + 2 > stopingCondition)
            {
                continue;
            } else
            {
                if (isBlockUnder)
                {
                    if (Random.Range(0, 100) > 30 + j * levelHeight * 0.1)
                    {
                        isBlockUnder = true;

                        GameObject block = Instantiate(classicBlock, new Vector2(i, j), Quaternion.identity);
                        block.transform.parent = this.transform;
                        block.GetComponent<SpriteRenderer>().material.mainTexture = theme.groundTextures[Random.Range(0, theme.groundTextures.Length)];
                    } else
                    {
                        isBlockUnder = false;

                        if (!isObstacleBefore)
                        {
                            if (Random.Range(0,100) > 25) 
                            {
                                isObstacleBefore = true;

                                GameObject obstacle = Instantiate(groundObstacle, new Vector2(i, j), Quaternion.identity);
                                obstacle.transform.parent = this.transform;
                                obstacle.GetComponent<SpriteRenderer>().material.mainTexture = theme.groundObstacleTextures[Random.Range(0, theme.groundObstacleTextures.Length)];
                            } else
                            {
                                isObstacleBefore = false;
                            }
                        } else
                        {
                            isObstacleBefore = false;
                        }
                    }
                }

            }
            // updating last J
            lastJ = j;
        }

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
                if (Random.Range(0, 100) > 10) 
                {
                    isCeilingNow = true;

                    GameObject ceilingBlock = Instantiate(classicBlock, new Vector2(i, startingJ + 1), Quaternion.identity);
                    ceilingBlock.transform.parent = this.transform;
                    ceilingBlock.GetComponent<SpriteRenderer>().material.mainTexture = theme.ceilingTextures[Random.Range(0, theme.ceilingTextures.Length)];
                    // updating last J
                    lastJ = startingJ + 1;
                }
            }
            else
            {
                if (Random.Range(0, 100) > 80)
                {
                    isCeilingNow = true;

                    GameObject ceilingBlock = Instantiate(classicBlock, new Vector2(i, startingJ + 1), Quaternion.identity);
                    ceilingBlock.transform.parent = this.transform;
                    ceilingBlock.GetComponent<SpriteRenderer>().material.mainTexture = theme.ceilingTextures[Random.Range(0, theme.ceilingTextures.Length)];
                    // updating last J
                    lastJ = startingJ + 1;
                }
            }
        }


        if (isCeilingNow)
        {
            if (Random.Range(0,100) > 65)
            {
                GameObject obstalce = Instantiate(ceilingObstalce, new Vector2(i, startingJ), Quaternion.identity);
                obstalce.GetComponent<SpriteRenderer>().material.mainTexture = theme.ceilingObstacleTextures[Random.Range(0, theme.ceilingObstacleTextures.Length)];
            }

            for (int j = startingJ + 2; j < levelHeight; j++)
            {
                int randomInt = Random.Range(0, 100);
                if (isCeilingBefore)
                {
                    if (randomInt > 10)
                    {

                    }
                }
                GameObject ceilingBlock = Instantiate(classicBlock, new Vector2(i, j), Quaternion.identity);
                ceilingBlock.transform.parent = this.transform;
                ceilingBlock.GetComponent<SpriteRenderer>().material.mainTexture = theme.ceilingTextures[Random.Range(0, theme.ceilingTextures.Length)];
                // updating last J
                lastJ = j;
            }
        }

        return isCeilingNow;
    }

    private void setBackgroundTexture(ThemeTemplate theme)
    {
        Image bgImage = backbground.GetComponent<Image>();
        bgImage.image = theme.background;
    }

    private void Start()
    {
        Debug.Log("oeoeoe");
        generate();
        Debug.Log("oeuf");

    }
}
