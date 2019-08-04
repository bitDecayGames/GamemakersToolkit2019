using UnityEngine;

public class ShootSkewerTest : MonoBehaviour {
    public SkewerThrower thrower;
    public BoardParser parser;


    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            var result = SkewerLogic.ShootSkewer(parser.ParseBoard(@"

##########
###{#u#}##
###[#u##}#
#P#u###]##
#[######]#
##########




", .8f, .8f), Directions.East, thrower);
            Debug.Log("Result: " + result.reason);
        }
    }
}