using Bounce.Unmanaged;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoHide
{
    public class AutoHide
    {
        public void ScratchPad()
        {
            Zone zone = BoardSessionManager.Board.GetZone(new Bounce.Mathematics.short3(0, 0, 0));
            var clients = BoardSessionManager.ClientDataModelIds.GetBuffer(out int _);
            foreach (var client in clients)
            {
                zone.MakeInactive(client.Item2);    //Will this show/hide zones per player? Maybe we don't even need hide volumes?
            }

            //Stolen from CreaturePerceptionManager.UpdateVisibilityForActivePartyImpl. There's some complex code there for line-of-sight stuff.
            NativeRefView<CreatureGuid> partyCreatureIds = CreatureManager.GetActivePartyCreatureIds();

            //Create a HideVolume at b, set it to active and add it to the board.
            Bounds b = new Bounds(new Vector3(0,0,0), new Vector3(1,1,1));
            HideVolume volume = new HideVolume(b, 0);
            HideVolumeManager.Instance.AddHideVolume(volume);
            volume.CloneSettingActive(true);
            HideVolumeManager.Instance.SetHideVolumeState(volume);
        }
    }
}
