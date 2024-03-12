public class DragObject : ActiveItem
{
    private void OnMouseDown()
    {
        Grab();
    }

    private void OnMouseDrag()
    {
        Drag();
        
    }

    private void OnMouseUp()
    {
        Release();
        
    }
}
