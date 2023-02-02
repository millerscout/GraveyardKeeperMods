using System;
using System.Collections.Generic;

namespace ZombiePlus
{
    public class HelperWorker
    {
        public static List<Worker> SavedWorkers;

        public static void UpdateWorkersWhitReloadFlag(ref bool flag)
        {
            Utilities.DelayerDelegateWithFlag(ref flag, delegate
            {
                SavedWorkersList workers = MainGame.me.save.workers;
                foreach (Worker savedWorker in SavedWorkers)
                {
                    SetWorkerEfficiency(workers.GetWorker(savedWorker.worker_unique_id));
                    SetWorkerSpeed(workers.GetWorker(savedWorker.worker_unique_id));
                }
            });
        }

        public static void UpdateWorkersWithReloadFlag(bool flag)
        {
            Utilities.DelayerDelegateWithFlag1(flag, delegate
            {
                SavedWorkersList workers = MainGame.me.save.workers;
                foreach (Worker savedWorker in SavedWorkers)
                {
                    SetWorkerEfficiency(workers.GetWorker(savedWorker.worker_unique_id));
                    SetWorkerSpeed(workers.GetWorker(savedWorker.worker_unique_id));
                }
            });
        }

        public static void SetWorkerSpeed(Worker _worker)
        {
            try
            {
                _worker.worker_wgo.data.SetParam("speed", Entry.Config.Zombie_MovementSpeed);
            }
            catch (Exception arg)
            {
                Entry.Log($"[SetWorkerSpeed] id={_worker.worker_unique_id} error {arg}");
            }
        }

        public static void SetWorkerEfficiency(Worker _worker)
        {
            try
            {
                Item data = _worker.worker_wgo.data;
                _worker.worker_wgo.data.GetBodySkulls(out var _, out var positive, out var _, dont_count_self: true);
                float num = Entry.Config.Zombie_BaseEfficiency * 100f / Entry.Config.Zombie_MaxEfficiency;
                float num2 = (float)positive + Entry.Config.Zombie_ExtraEfficiency;
                data.SetParam("working_k", num2 / num);
            }
            catch (Exception arg)
            {
                Entry.Log($"[SetWorkerSpeed] id={_worker.worker_unique_id} error {arg}");
            }
        }
    }
}
