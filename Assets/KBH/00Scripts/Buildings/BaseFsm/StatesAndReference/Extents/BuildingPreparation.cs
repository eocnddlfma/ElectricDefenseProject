

class BuildingPreparation : State<BuildingBaseStateEnum>
{
   public override bool CanChangeToOther(ref BuildingBaseStateEnum state)
   {
      return false;
   }
}
