import UIKit
import UniformTypeIdentifiers    // iOS 14+
import QuickLook                 // Quick Look preview support

// ----------------------------------------------------------------------
// Unity ↔︎ Swift bridge
// ----------------------------------------------------------------------

// Callbacks from Swift → C (Unity)
@_silgen_name("UnitySendMessage")
func UnitySendMessage(_ obj: UnsafePointer<Int8>,
                      _ method: UnsafePointer<Int8>,
                      _ msg: UnsafePointer<Int8>) -> Void

// Callbacks from C# → Swift (DllImport symbols)
@_cdecl("pick")        public func pick()        { FilesBridge.pick() }
@_cdecl("shareFile")   public func shareFile(_ cPath: UnsafePointer<Int8>)   { FilesBridge.shareFile(cPath) }
@_cdecl("previewFile") public func previewFile(_ cPath: UnsafePointer<Int8>) { FilesBridge.previewFile(cPath) }

// ----------------------------------------------------------------------
// Main helper class
// ----------------------------------------------------------------------
@objc public class FilesBridge: NSObject, UIDocumentPickerDelegate {

    static let shared = FilesBridge()

    // ------------------------------------------------------------------
    // PICK
    // ------------------------------------------------------------------
    @objc public static func pick() {
        shared.startPick()
    }
    private func startPick() {
        let picker = UIDocumentPickerViewController(
            forOpeningContentTypes: [UTType.data],   // narrow if you wish
            asCopy: false)
        picker.delegate = self
        picker.modalPresentationStyle = .fullScreen
        FilesBridge.rootVC.present(picker, animated: true)
    }

    // ------------------------------------------------------------------
    // SHARE
    // ------------------------------------------------------------------
    @objc public static func shareFile(_ cPath: UnsafePointer<Int8>) {
        let url = URL(fileURLWithPath: String(cString: cPath))
        let vc  = UIActivityViewController(activityItems: [url], applicationActivities: nil)
        FilesBridge.rootVC.present(vc, animated: true)
    }

    // ------------------------------------------------------------------
    // UIDocumentPicker delegates
    // ------------------------------------------------------------------
    public func documentPicker(_ controller: UIDocumentPickerViewController,
                               didPickDocumentsAt urls: [URL]) {
        if let url = urls.first {
            UnitySendMessage("BackupCodeLogic", "OnFilePicked", url.path)
        }
    }
    public func documentPickerWasCancelled(_ controller: UIDocumentPickerViewController) {
        UnitySendMessage("BackupCodeLogic", "OnFilePicked", "")
    }

    // ------------------------------------------------------------------
    // Root VC helper (STATIC PROPERTY)
    // ------------------------------------------------------------------
    private static var rootVC: UIViewController {
        UIApplication.shared.connectedScenes
                     .compactMap { ($0 as? UIWindowScene)?
                                    .windows.first?
                                    .rootViewController }
                     .first!
    }
}

// ----------------------------------------------------------------------
// Quick Look preview
// ----------------------------------------------------------------------
extension FilesBridge: QLPreviewControllerDataSource {

    @objc public static func previewFile(_ cPath: UnsafePointer<Int8>) {
        let path = String(cString: cPath)
        shared.previewURL = URL(fileURLWithPath: path)
        shared.showPreview()
    }

    // -- internal helpers --
    private struct Holder { static let ctrl = QLPreviewController() }
    private var preview: QLPreviewController { Holder.ctrl }

    private var previewURL: URL? {
        get { objc_getAssociatedObject(self, &previewKey) as? URL }
        set { objc_setAssociatedObject(self, &previewKey, newValue, .OBJC_ASSOCIATION_RETAIN_NONATOMIC) }
    }

    private func showPreview() {
        preview.dataSource = self
        preview.currentPreviewItemIndex = 0
        FilesBridge.rootVC.present(preview, animated: true)
    }

    // -- QLPreviewControllerDataSource --
    public func numberOfPreviewItems(in _: QLPreviewController) -> Int { 1 }
    public func previewController(_ controller: QLPreviewController,
                                  previewItemAt index: Int) -> QLPreviewItem {
        return previewURL! as NSURL
    }
}

private var previewKey: UInt8 = 0