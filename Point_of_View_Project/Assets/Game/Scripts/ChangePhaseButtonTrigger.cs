using UnityEngine;

namespace Game.Scripts
{
    public class ChangePhaseButtonTrigger: MonoBehaviour
    {
        
        //[SerializeField] private Color color;
        [SerializeField] private MultiPlatformManager multiPlatformManager;
        [SerializeField] private int phase;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            if (phase<1 || phase>4)
            {
                return;
            }
            
            switch (phase)
            {
                case 1:
                    multiPlatformManager.changePhase(MultiPlatformManager.Phase.P1);
                    break;
                case 2:
                    multiPlatformManager.changePhase(MultiPlatformManager.Phase.P2);
                    break;
                case 3:
                    multiPlatformManager.changePhase(MultiPlatformManager.Phase.P3);
                    break;
                case 4:
                    multiPlatformManager.changePhase(MultiPlatformManager.Phase.P4);
                    break;
            }
            //transform.GetComponent<Light>().color = color;
        }
    }
}
