#import "IAPInterface.h"
#import "IAPManager.h"

@implementation IAPInterface

IAPManager *iapManager = nil;
//初始化商品信息
void InitIAPManager(){
    iapManager = [[IAPManager alloc] init];
    [iapManager attachObserver];
    
}
//判断是否可以购买
bool IsProductAvailable(){
    return [iapManager CanMakePayment];
}
//获取商品信息
void RequstProductInfo(void *p){
    NSString *list = [NSString stringWithUTF8String:p];
    NSLog(@"商品列表:%@",list);
    [iapManager requestProductData:list];
}
//购买商品
void BuyProduct(void *p){
    [iapManager buyRequest:[NSString stringWithUTF8String:p]];
}

@end
