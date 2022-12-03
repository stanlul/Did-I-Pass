using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursor;

    public Texture2D cursorClicked;

    private CursorControls controls;

    private Camera mainCamera;

    private void Awake()
    {
        controls = new CursorControls();
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controls.Mouse.Click.started += _ => StartedClick();
        controls.Mouse.Click.performed += _ => EndedClick();
    }
    private void StartedClick()
    {
        ChangeCursor(cursorClicked);
    }
    private void EndedClick()
    {
        ChangeCursor(cursor);
        DetectObject();
    }
    private void DetectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());
        //RaycastHit hit;
        //if(Physics.Raycast(ray, out hit))
        //{
        //    if(hit.collider == null)
        //    {
        //        Debug.Log("1");
        //    }
        //}

        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
        if (hit2D.collider != null)
        {
            IClicked clicked = hit2D.collider.gameObject.GetComponent<IClicked>();
            if (clicked != null) clicked.OnClickAction();
            Debug.Log("hit");
        }

    }

    private void ChangeCursor(Texture2D cursorType)
    {
        Cursor.SetCursor(cursorType,Vector2.zero,CursorMode.Auto);
    }
    
}
