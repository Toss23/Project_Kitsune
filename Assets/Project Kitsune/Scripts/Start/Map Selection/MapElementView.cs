using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class MapElementView : MonoBehaviour
{
    [SerializeField] private EnemySpawnerInfo _enemySpawnerInfo;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _statsText;
    [SerializeField] private Button _enterButton;

    private IContext _context;

    private void Awake()
    {
        _enterButton.onClick.AddListener(OnClickEnter);
    }

    private void OnClickEnter()
    {
        _context.PauseGame();
    }

    public void SetContext(IContext context)
    {
        if (_context == null)
        {
            _context = context;
        }
    }

    private void Update()
    {
        if (_enemySpawnerInfo != null)
        {
            if (_nameText != null)
            {
                _nameText.text = _enemySpawnerInfo.Name;
            }

            if (_iconImage != null)
            {

            }

            if (_statsText != null)
            {
                _statsText.text = "Map stats\n---\n";
                _statsText.text += "Waves: " + _enemySpawnerInfo.SpawnRules.Length + "\n";
                float endTime = _enemySpawnerInfo.SpawnRules[_enemySpawnerInfo.SpawnRules.Length - 1].EndTime;
                int time = Mathf.RoundToInt(endTime / 60f * 10f) / 10;
                _statsText.text += "Time: ~" + time + " min";
            }
        }
    }
}