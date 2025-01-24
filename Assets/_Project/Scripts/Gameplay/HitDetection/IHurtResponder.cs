namespace MonkeyBusiness.Gameplay.HitDetection
{
    public interface IHurtResponder
    {
        public void Response(HitData data);

        public bool CheckHit(HitData data);
    }
}