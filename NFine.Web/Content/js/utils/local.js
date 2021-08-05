// ȫ���û���Ϣ
var USER_INFO = {};
// ��ȡ����
function getStorage(key) {
    return JSON.parse(localStorage.getItem(key));
}
// �洢����
function setStorage(key, value) {
    return localStorage.setItem(key, JSON.stringify(value));
}
