using Assets.Lesson_1.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*----------------------------------------------------------------------------------------
 *								C# NOTES: 'ATTRIBUTES'
 * ---------------------------------------------------------------------------------------
 * 'Attributes' are special types of classes which can add metadata to classes, functions, 
 * fields, etc.  The meaning of this metadata is typically defined by the program executing
 * the decorated code.  
 * 
 * Note that not all attributes can be applied to all types of declarations.  For example,
 * the 'RequireComponent' attribute defined by the Unity engine can ONLY be used to decorate
 * 'class' declarations.  
 * 
 * To learn more about C# attributes and their usage, see: 
 *	https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/attributes/
 *	https://docs.microsoft.com/en-us/dotnet/api/system.attributeusageattribute
 *	
 * ---------------------------------------------------------------------------------------
 */

/* ----------------------------------------------------------------------------------------
 *							UNITY NOTES: 'UNITY ATTRIBUTES'
 * ----------------------------------------------------------------------------------------
 * The 'ExecuteAlways' attribute tells Unity that the decorated member should be executed
 * even outside of 'Play' mode.  Typically, scripts only execute while the game is playing.
 * However, the engine itself is capable of executing your code outside of play mode and will
 * do so for any class, method, or member decorated with the 'ExecuteAlways' attribute.
 * 
 * The 'RequireComponent' attribute can only be used to decorate component classes.
 * It tells Unity that any game object which includes the component must also include
 * a component of the specified type.  If a component of the required type does not 
 * already exist on the game object, Unity will automatically add one.
 * 
 * This is useful for reducing redundant null checking when your component assumes
 * that another component should always exist, but use with caution as you may find 
 * yourself adding unnecessary components to your objects which can't be removed.
 * 
 * To learn more about Unity attributes you can use in your code, see:
 *	https://docs.unity3d.com/Manual/Attributes.html
 */
[ExecuteAlways]
[RequireComponent(typeof(MeshRenderer))]
public class ColorReceiverComponent : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------
	 *								C# NOTES: 'FIELDS'
	 * ---------------------------------------------------------------------------------------
	 * 
	 * The following 3 declarations are known in C# as 'fields'.  Fields are the most basic 
	 * way of declaring variables scoped to a 'class'.  Fields are declared by specifying
	 * a 'type' (what can be stored in the variable) and an optional 'access modifier'.
	 * 
	 * An 'access modifier' determines how accessible a class member (field, method, etc.) is.
	 * By default, a field declared in a class is 'private' which means it is ONLY accessible
	 * from within the declaring class.  A field can also be marked 'protected' which means it
	 * is accessible to the declaring class and any derived classes or 'public' meaning it can
	 * be accessed from anywhere, provided a valid instance of the declaring class is available.
	 * 
	 * Although marking fields as 'private' is not strictly necessary, it is best practice to
	 * always be explicit when declaring members.  It is also good practice to use camelCase 
	 * and to prefix protected and private fields with an underscore.  (Ex. _myVar)
	 * 
	 * In Unity, a field declared on a 'MonoBehaviour component' can be exposed in the 'Inspector'
	 * panel by either making it 'public' or applying the 'SerializeField' attribute (shown below).
	 * 
	 * To learn more about C# fields, see:
	 *	https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/fields
	 * ---------------------------------------------------------------------------------------
	 */

	//The '_meshRenderer' field IS NOT exposed in the Inspector panel because it is 'private'.
	private MeshRenderer _meshRenderer;

	//The 'provider' field IS exposed in the Inspector because it is marked 'public'.
	public ColorProviderComponent provider;

	//Although the '_providerObject' field is 'private', it IS exposed in the 
	//Inspector because it is decorated with the 'SerializeField' attribute.
	[SerializeField]
	private GameObject _providerObject;


	/*----------------------------------------------------------------------------------------
	 *							UNITY NOTES: 'EVENT HOOKS'
	 *----------------------------------------------------------------------------------------
	 *	Unity has a number of 'events' which are invoked at different points within the execution
	 *	of the game.  Most of these occur once or more per frame (Update, FixedUpdate, etc.), 
	 *	some occur at certain points in the game's lifecycle (Awake, Start, OnApplicationQuit, 
	 *	etc.), while others occur as a result of something happening in your game (OnCollisionEnter, 
	 *	OnTriggerEnter, etc.).  
	 *	
	 *	Each of these has their own unique use cases, but the ones you will use most commonly are
	 *	'Awake' and 'Start' (for initialization of most state) and 'Update' (for logic you need 
	 *	to run each frame).  
	 *	
	 *	The 'OnGUI' event hook below is executed in response to GUI events which require the UI to 
	 *	be re-drawn.  We use it in conjunction with the 'ExecuteAlways' attribute at the class level
	 *	to enable updating of the receiver's mesh color in the editor in response to changes.
	 *	
	 *	The 'Awake' event hook runs once when the component is instantiated on its respective 
	 *	game object. Because of the 'ExecuteAlways' attribute, this event will occur when entering 
	 *	and exiting play mode.
	 *	
	 *	To learn more about Unity events and their lifecycle, see:
	 *	  https://docs.unity3d.com/Manual/ExecutionOrder.html
	 *----------------------------------------------------------------------------------------
	 */
	private void Awake()
	{
		/*----------------------------------------------------------------------------------------
		 *							UNITY NOTES: 'FINDING COMPONENTS'
		 * ---------------------------------------------------------------------------------------
		 * Since there are two ways in the editor to assign a reference to the provider component
		 * (either directly or indirectly through its parent game object), we first check if the
		 * 'provider' field is assigned.  If it's not, we will check whether the '_providerObject'
		 * field is assigned and (if so) we will try to find a 'ColorProviderComponent' on it.
		 * 
		 * In the Unity engine, both the 'GameObject' and 'MonoBehaviour' base classes expose a
		 * set of methods for locating game objects in their relative hierarchical structure.
		 * These include methods for searching a game object directly, as well as some for
		 * searching the object's parent object(s) or children.
		 * 
		 * These methods require the 'type' of component to search for which can either be
		 * provided as a string parameter or a 'generic type parameter' (the type specified 
		 * in angle brackets below).  Note that compile-time types or constants should be 
		 * favored over hard-coded 'magic' string values as a general rule-of-thumb.
		 * 
		 * In the case of 'MonoBehaviour' derived components, Unity asserts that all components
		 * MUST exist on a game object, so calling any of these methods directly within a 
		 * component script (without a specific object reference as shown below), will search 
		 * relative to the game object on which the script is instantiated.  
		 * 
		 * For example, when the 'ColorReceiverComponent' is instantiated on our 'ReceiverCube' 
		 * in the scene, the call to 'GetComponent<MeshRenderer>()' searches for a 'MeshRenderer'
		 * component on the 'ReceiverCube'.  Since we decorated the 'ColorReceiverComponent'
		 * with a 'RequireComponent' attribute for the same type, we can guarantee that the
		 * requested component type exists on the 'ReceiverCube' and that it will be retrieved
		 * successfully.  
		 * 
		 * If the 'MeshRenderer' component were not required, it would be possible to add the
		 * 'ColorReceiverComponent' to a game object that did not also have a 'MeshRenderer' 
		 * attached.  In such a scenario, the call to 'GetComponent<MeshRenderer>()' would
		 * return 'null'.
		 * 
		 * To learn more about getting components, see the 'Public Methods' section under:
		 *	https://docs.unity3d.com/ScriptReference/GameObject.html
		 * ----------------------------------------------------------------------------------------
		 */
		_meshRenderer = GetComponent<MeshRenderer>();
	}


	void OnGUI()
	{
		/* ---------------------------------------------------------------------------------------
		 *							UNITY NOTES: 'TRYGETCOMPONENT'
		 * ---------------------------------------------------------------------------------------
		 * Since there are two ways in the editor to assign a reference to the provider component
		 * (either directly or indirectly through its parent game object), we first check if the 
		 * 'provider' field is assigned.  If it's not, we will check whether the '_providerObject' 
		 * field is assigned and (if so) we will try to find a 'ColorProviderComponent' on it.
		 * 
		 * The 'TryGetComponent' method is a special component searching method which can be used 
		 * when you aren't sure whether a certain type of component will be present on a game object.
		 * Unlike other search methods, 'TryGetComponent' does not return the component directly.
		 * Instead, it returns a boolean value indicating whether a component was found.  
		 * 
		 * The method also requires a variable of the target component type which is passed as a
		 * parameter using the 'out' keyword.  The 'out' keyword in this context designates that
		 * the parameter is an 'output' parameter, meaning the method being called MUST assign a
		 * value to it before it returns.  Note that if a component is NOT found with this method,
		 * the value assigned to the provided variable will be 'null'.
		 * 
		 * It should also be noted that 'TryGetComponent' DOES expect a generic type parameter
		 * (to know the type of component for which to search).  However, since the type of the
		 * 'provider' variable is known at compile time, the method is able to 'infer' the 
		 * generic type parameter from the supplied parameter.
		 * 
		 * To learn more about the 'TryGetComponent' method, see:
		 *	https://docs.unity3d.com/ScriptReference/Component.TryGetComponent.html
		 *	
		 * To learn more about (the more advanced concept of) generics in C# , see:
		 *	https://docs.microsoft.com/en-us/dotnet/standard/generics/
		 * ---------------------------------------------------------------------------------------
		 */
		if
		(
			provider != null ||
			(
				_providerObject != null &&
				_providerObject.TryGetComponent(out provider)
			)
		)
		{
			/* ---------------------------------------------------------------------------------------
			 *								C# NOTES: 'SWITCH STATEMENTS'
			 * ---------------------------------------------------------------------------------------
			 * A switch statement is a type of conditional logic block (similar to an 'if') which
			 * allows one or more blocks of code to be conditionally executed based on a given value.
			 * 
			 * Unlike 'if', 'else if', 'else' style conditional, switch cases aren't evaluated 
			 * sequentially until a condition evaluates to 'true'.  Instead, execution jumps directly 
			 * to the matching condition (if one exists).  Since control cannot fall-through to
			 * subsequent case statements, each block must be terminated with the 'break' keyword.
			 * 
			 * If no case is found which matches the provided statement, a 'default' case can also
			 * be defined (as shown below).  Regardless of where this case appears, it is evaluated
			 * last.  However, best practice is to define the 'default' case at the end of the switch.
			 * 
			 * If multiple cases should execute the same block of code (as shown below), they can
			 * be 'stacked' on top of one another, provided they are followed by a statement block.
			 * 
			 * To learn more about switch statements, see:
			 *	https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements
			 * ---------------------------------------------------------------------------------------
			 */
			switch (provider.cubeColor)
			{
				case PrimaryColor.Red:
				{
					_meshRenderer.sharedMaterial.color = Color.red;
					break;
				}

				case PrimaryColor.Green:
				{
					_meshRenderer.sharedMaterial.color = Color.green;
					break;
				}

				case PrimaryColor.Blue:
				{
					_meshRenderer.sharedMaterial.color = Color.blue;
					break;
				}

				case PrimaryColor.None:
				default:
				{
					_meshRenderer.sharedMaterial.color = Color.gray;
					break;
				}
			}
		}
	}
}
