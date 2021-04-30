using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EnemyMove))]
public class EnemyMoveEditor : Editor
{

	void OnSceneGUI()
	{
		
		//FieldOfView fow = (FieldOfView)target;
		//Handles.color = Color.white;

		////ViewRadius 만큼의 원을 그린다.
		//Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
		//Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
		//Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

		////중앙부터 원까지 선을 그린다(viewRadius에 따라)
		//Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
		//Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

		//Handles.color = Color.red;
		//foreach (Transform visibleTarget in fow.visibleTargets)
		//{
		//	Handles.DrawLine(fow.transform.position, visibleTarget.position);
		//}
	}

}
