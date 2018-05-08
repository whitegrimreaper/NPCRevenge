using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour {

	public int balance_;

	public const int MAX_BALANCE = 99999999;

	//Withdrawls Money from your balance
	//Return Codes:
	//	1 - Not enough funds
	//	0 - Successful
	/*public short Withdrawl(uint amount){
		if (balance_ >= amount){
			balance_ -= amount;
			Debug.Log(DisplayBalance());
			return 0;
		}
		else{
			Debug.Log("Insufficient Funds");
			return 1;
		}
	}

	//Removes money from balance
	//Return Codes:
	//	1 - Reached Maximum balance
	//	0 - Successful
	public void Deposit(uint amount){
		if (MAX_BALANCE >= (balance_ + amount)){
			balance_ += amount;
			Debug.Log(DisplayBalance());
		}
		else{
			balance_ = MAX_BALANCE;
			Debug.Log("Maximum Balance.\n" + DisplayBalance());
		}
	}

	//Displays balance as a combination of different currencies
	public String DisplayBalance(){
		uint temp_val = balance_;
		uint copper = temp_val % 100;
		uint silver = (temp_val = (temp_val - copper) / 100) % 100; 
		uint gold = ((temp_val = (temp_val - silver) / 100) % 100);
		uint platinum = ((temp_val = (temp_val - gold) / 100) % 100);

		return String.Format("{0}'s Balance: {1}pp {2}gp {3}sp {4}cp",
							 transform.gameObject.name,
							 platinum,
							 gold,
							 silver,
							 copper);
	}*/
}
