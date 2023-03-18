### Tl;Dr

Inheritance is basically saying that the child class you're making should have all of the variables and features of a parent class from which it is extending.

This adds the `protected` access level (like private and public) that allows child classes to access a variable while still leaving it private to everything external to the inheritance chain.

`protected` and `public` methods can also be overridden, by being marked as `virtual` in the parent class. Then, add the same function to the child class with the keyword `override`. If you need to call the parent class's version of the method (like if you're just modifying the parameters and then sending them along, so as not to rewrite logic needlessly), you can call `base.TheMethodsName()`.

### More in-depth

As you are already aware, we can limit the repetition of code shared across similar systems by creating a base class and having other classes inherit from it. That looks like this:

```csharp
public class Character {
	protected int m_health;
	protected List<Item> m_inventory;
	
	public void TakeDamage(int damageAmount) {
		//logic for taking damage
	}
}

public class Ally : Character {
	//Ally has a TakeDamage() method without us having to rewrite it

	public void GiveItem(Item item) {
		//logic for adding item to m_inventory, which we also get via inheritance
	}
}

public class Enemy : Character {
	//Enemy has a TakeDamage() method without us having to rewrite it

	public void PickPocket(Item item) {
		//logic for attempting to take item from m_inventory
	}
}
```

Note that the m_health and m_inventory variables are `protected`, allowing child classes like Ally and Enemy to access and modify it, while disallowing external code to modify it. If they were `private`, Ally and Enemy would not be able to touch them.

### Virtual and Override

What if we wanted to change the way in which `TakeDamage()` works in a child class? We can do this by overriding the `TakeDamage()` method.

First, we must mark `TakeDamage()` as virtual in the base `Character` class, which tells the compiler child classes have permission to make changes.

```csharp
public class Character {
	protected int m_health;
	protected List<Item> m_inventory;
	
	public virtual void TakeDamage(int damageAmount) {
		//logic for taking damage
	}
}
```

The only change here is that we added `virtual` between `public` and `void`. Now we can `override` that method in our child class, modifying the damageAmount by some armor value, before sending it to the base class to be processed as normal.

```csharp
public class Ally : Character {
	private int m_armor;
	
	public override void TakeDamage(int damageAmount) {
		damageAmount -= m_armor;
		base.TakeDamage(damageAmount);
	}

	public void GiveItem(Item item) {
		//logic for adding item to m_inventory
	}
}
```

Note: This cannot be done for `private` functions, only `public` and `protected`. If you need to override a `private` method, change it to `protected`.