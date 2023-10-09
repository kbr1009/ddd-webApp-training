using TESTWebApp.Domain.Models.Users;

namespace TESTWebApp.Domain.Services.Users
{
    public sealed class UserService
    {
        /// <summary>
        /// ユーザリポジトリインターフェース
        /// </summary>
        private readonly IUserRepository _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="repository">リポジトリ</param>
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// ユーザの存在を確認する
        /// </summary>
        /// <param name="user">ユーザのドメインモデル</param>
        /// <returns>true: 存在する false; 存在しない</returns>
        public bool IsDupulicatedUser(User user)
        {
            var duplicatedUser = _repository.FindByUserName(user.UserName.Value);
            return duplicatedUser != null;
        }

        /// <summary>
        /// DBにと六されているWebセッションがクライアントに登録しているWebセッションと同一か
        /// </summary>
        /// <param name="userId">ユーザIDドメインモデル</param>
        /// <param name="webSessionId">WebセッションIDドメインモデル</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool WebSessionIsMach(UserId userId, WebSessionId webSessionId)
        {
            var userData = _repository.FindByUserId(userId.Value) 
                ?? throw new ArgumentException("ユーザの参照に失敗しました。");

            return userData.WebSessionId.Equals(webSessionId);
        }
    }
}
