using UnityEngine;
public class Navigation : MonoBehaviour
{

    public GameObject particle = null;//Prefab物体，用来点击地图以后作为临时生成物指示寻路目标地点
    protected UnityEngine.AI.NavMeshAgent agent;
    protected Animator animator;
    protected Object particleClone;//prefab的临时生成物的引用


    // Use this for initialization
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;//不使用NavMeshAgent组件的导路时的方向


        animator = GetComponent<Animator>();

    }

    protected void SetDestination()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);//获取穿过摄像机和鼠标点击位置的射线
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))//射线碰撞检测
        {
            if (particleClone)//再次点击之后，销毁之前点击生成的物体
            {

                GameObject.Destroy(particleClone);
                particleClone = null;
            }

            //function SetLookRotation (view : Vector3, up : Vector3 = Vector3.up) : void 
            // 建立一个旋转,使z轴朝向view ,y轴朝向up  
            Quaternion q = new Quaternion();
            q.SetLookRotation(hit.normal, Vector3.forward);
            particleClone = Instantiate(particle, hit.point, q);



            //设置或更新的目标。这会触发一个新的路径计算。
            agent.destination = hit.point;
        }
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetDestination();
        }
    }


}
