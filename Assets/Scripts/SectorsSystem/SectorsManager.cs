using UnityEngine;

namespace SectorsSystem
{
    public class SectorsManager : MonoBehaviour
    {
        public Color playerSectorColor, enemySectorColor;
        
        [SerializeField] private SectorsInfo sectorsInfo;
        public Sector ChosenSector { get; private set; }

        public Sector sectorOnWhichWasPointer;
        
        public static SectorsManager Instance;

        private void Awake() => Instance = this;

        public void PrepareForMovingTroops(out Sector sector)
        {
            ChosenSector.DisableSectorInteraction();
            sectorsInfo.Close();

            sector = ChosenSector;
            ChosenSector = null;
        }

        public void EndMovingTroops()
        {
            if (ChosenSector != null)
            {
                ChosenSector.DisableSectorInteraction();
                ChosenSector = null;
            }
        }

        public void ChoseTheSector(Sector sector)
        {
            if (SectorsTroopsMove.Instance.isMoveModeOn)
            {
                if (sector == SectorsTroopsMove.Instance.GetFromSector()) return;
                
                if (ChosenSector != null) ChosenSector.DisableSectorInteraction();
                
                SectorsTroopsMove.Instance.SetDestination(sector);
                
                sector.EnableSectorInteraction(true);

                ChosenSector = sector;
                
                return;
            }
            
            if (ChosenSector == null) //Choose first sector
            {
                ChosenSector = sector;
                ChosenSector.EnableSectorInteraction(false);
            
                sectorsInfo.Show(ChosenSector.troopsCount);
            }
            else if (ChosenSector == sector)
            {
                ChosenSector.DisableSectorInteraction();
                ChosenSector = null;

                sectorsInfo.Close();
            }
            else
            {
                ChosenSector.DisableSectorInteraction();
                ChosenSector = sector;
                ChosenSector.EnableSectorInteraction(false);

                sectorsInfo.Show(ChosenSector.troopsCount);
            }
        }
    }
}