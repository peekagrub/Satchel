using Modding;
using UnityEngine;

namespace DandyCore{
    public class AlertRangeMarker : MonoBehaviour{

        // dont show in editer
        private bool processed = false;

        public bool isProcessed(){
            return processed;
        }
        public void setProcessed(){
            processed = true;
        }
    }
}