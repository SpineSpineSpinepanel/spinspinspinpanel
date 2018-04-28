
public interface IPattern
{
    void OnStart();
    void OnUpdate(float deletaTime);
    void OnEnd();

    bool IsTweening();
    int GetTotalBallCount();
}
