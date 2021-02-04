using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
	public void Lvl1(){
		SceneManager.LoadScene("lvl_1");
	}
	public void Lvl2(){
		SceneManager.LoadScene("lvl_2");
	}
	public void Lvl3(){
		SceneManager.LoadScene("lvl_3");
	}
	public void main_menu(){
		SceneManager.LoadScene("main_menu");
	}
	public void BackToMenu(){
		SceneManager.LoadScene ("main_menu");
	}
}