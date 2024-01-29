using UnityEngine;

public class SectorsManager : MonoBehaviour
{
    [SerializeField] private Sector chosenSector;

    public static SectorsManager Instance;

    private void Awake() => Instance = this;

    public void ChoseTheSector(Sector sector)
    {
        if (chosenSector == null)
        {
            chosenSector = sector;
            chosenSector.EnableSectorInteraction();
        }
        else if (chosenSector == sector)
        {
            chosenSector.DisableSectorInteraction();
            chosenSector = null;
        }
        else
        {
            chosenSector.DisableSectorInteraction();
            chosenSector = sector;
            chosenSector.EnableSectorInteraction();
        }
    }
}