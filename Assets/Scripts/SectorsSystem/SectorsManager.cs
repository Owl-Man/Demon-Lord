using UnityEngine;

namespace SectorsSystem
{
    public class SectorsManager : MonoBehaviour
    {
        [SerializeField] private SectorsInfo sectorsInfo;
        [SerializeField] private Sector chosenSector;

        public Sector sectorOnWhichWasPointer;
        
        public static SectorsManager Instance;

        private void Awake() => Instance = this;

        public void ChoseTheSector(Sector sector)
        {
            if (chosenSector == null) //Choose first sector
            {
                chosenSector = sector;
                chosenSector.EnableSectorInteraction();
            
                sectorsInfo.Show(chosenSector.armyCount.ToString());
            }
            else if (sector.isSectorOccupied) 
            {
                if (chosenSector == sector)
                {
                    chosenSector.DisableSectorInteraction();
                    chosenSector = null;

                    sectorsInfo.Close();
                }
                else
                {
                    chosenSector.DisableSectorInteraction();
                    chosenSector = sector;
                    chosenSector.EnableSectorInteraction();

                    sectorsInfo.Show(chosenSector.armyCount.ToString());
                }
            }
            else
            {
                //Attack other sector
            }
        }
    }
}