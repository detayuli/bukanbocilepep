using UnityEngine;

public class BlowAndRelease : MonoBehaviour
{
    public GameObject bubblePrefab; // Prefab untuk gelembung
    public Transform bubbleSpawnPoint; // Posisi awal gelembung
    public float blowSpeed = 0.1f; // Kecepatan meniup gelembung
    public Vector3 baseMaxSize = new Vector3(2.0f, 2.0f, 2.0f); // Batas maksimum tetap untuk ukuran gelembung
    public Vector3 incrementPerShake = new Vector3(0.1f, 0.1f, 0.1f); // Kenaikan batas ukuran per kocokan sabun
    public float releaseSpeed = 2f; // Kecepatan terbang gelembung saat dilepas
    public GameObject explosionEffectPrefab; // Prefab efek ledakan

    private GameObject currentBubble; // Referensi ke gelembung yang sedang ditiup
    private Vector3 currentMaxSize; // Ukuran maksimum untuk gelembung saat ini
    private bool isBlowing = false; // Apakah tombol tiup sedang ditekan
    private bool canRelease = false; // Apakah gelembung dapat dilepas
    private bool isSoapShaken = false; // Apakah sabun sudah dikocok (tombol lepas ditekan)
    private const float tolerance = 0.01f; // Toleransi untuk perbandingan skala

    private int shakeCount = 0; // Jumlah sabun dikocok
    private int maxShakes = 16; // Maksimal kocokan sabun
    private float chance = 100f; // Nilai Chance awal

    void Update()
    {
        // Tombol untuk meniup gelembung
        if (Input.GetKey(KeyCode.J))
        {
            BlowBubble();
        }
        else
        {
            isBlowing = false; // Reset status meniup jika tombol dilepas
        }

        // Tombol untuk melepas gelembung
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (currentBubble != null && !isBlowing && canRelease)
            {
                ReleaseBubble(); // Melepaskan gelembung
            }
            else if (currentBubble == null && shakeCount < maxShakes)
            {
                ShakeSoap(); // Mengocok sabun jika tidak ada gelembung
            }
        }
    }

    void BlowBubble()
    {
        // Jika tidak ada gelembung, buat satu
        if (currentBubble == null && isSoapShaken)
        {
            CreateNewBubble();
        }

        // Perbesar gelembung hingga ukuran maksimum
        Vector3 currentScale = currentBubble.transform.localScale;

        if (currentScale.x < currentMaxSize.x - tolerance &&
            currentScale.y < currentMaxSize.y - tolerance &&
            currentScale.z < currentMaxSize.z - tolerance)
        {
            currentBubble.transform.localScale += Vector3.one * blowSpeed * Time.deltaTime;
            isBlowing = true;
            canRelease = true; // Setelah meniup, gelembung dapat dilepas
        }
        else
        {
            // Jika gelembung mencapai atau melewati ukuran maksimum, cek ledakan
            CheckExplosion();
        }
    }

    void ReleaseBubble()
    {
        if (currentBubble != null)
        {
            // Tambahkan rigidbody untuk membuat gelembung terbang ke atas
            Rigidbody rb = currentBubble.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.linearVelocity = Vector3.up * releaseSpeed;

            // Reset variabel
            currentBubble = null;
            canRelease = false;
        }
    }

    void ShakeSoap()
    {
        // Mengocok sabun: Set status bahwa sabun telah dikocok
        shakeCount++;
        chance -= 5; // Kurangi chance setiap kocokan
        isSoapShaken = true;

        // Tingkatkan ukuran maksimum gelembung dengan batas
        currentMaxSize = baseMaxSize + incrementPerShake * shakeCount;

        Debug.Log($"Soap shaken {shakeCount} times. New max bubble size: {currentMaxSize}. Chance: {chance}");
    }

    void CheckExplosion()
    {
        float holdValue = Random.Range(0f, 100f);

        if (chance > holdValue)
        {
            ExplodeBubble();
        }
        else
        {
            Debug.Log("Bubble is safe.");
        }
    }

    void ExplodeBubble()
    {
        if (currentBubble != null)
        {
            // Tambahkan efek ledakan jika ada
            if (explosionEffectPrefab != null)
            {
                Instantiate(explosionEffectPrefab, currentBubble.transform.position, Quaternion.identity);
            }

            // Hancurkan gelembung
            Destroy(currentBubble);
            currentBubble = null;

            // Reset variabel
            isBlowing = false;
            canRelease = false;

            // Reset sabun jika gelembung meledak
            isSoapShaken = false;

            Debug.Log("Bubble exploded!");
        }
    }

    void CreateNewBubble()
    {
        // Buat gelembung baru
        currentBubble = Instantiate(bubblePrefab, bubbleSpawnPoint.position, Quaternion.identity);

        // Tetapkan ukuran maksimum awal untuk gelembung baru
        currentMaxSize = baseMaxSize + incrementPerShake * shakeCount;

        Debug.Log($"New bubble max size: {currentMaxSize}");
    }
}
